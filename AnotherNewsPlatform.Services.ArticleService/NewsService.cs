using AnotherNewsPlatform.Core.DTOs;
using AnotherNewsPlatform.Database;
using AnotherNewsPlatform.CQS;
using AnotherNewsPlatform.Database.Entities;
using Serilog;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using MediatR;
using AnotherNewsPlatform.CQS.Articles.Commands;
using AnotherNewsPlatform.CQS.Articles.Query;

namespace AnotherNewsPlatform.Services.NewsService
{
    public class NewsService(
        IMediator mediator,
        AnpDbContext dbContext,
        IRssReader rssReader,
        IWebScraper webScraper) : INewsService
    {
        public async Task<List<ArticleDto>> GetNewsAsync(CancellationToken cancellationToken)
        {
            var result = await dbContext.Articles
                .AsNoTracking()
                .Include(n => n.Source)
                .OrderByDescending(n => n.PublishDate)
                .Select(n => new ArticleDto
                {
                    Id = n.Id,
                    Title = n.Title,
                    Content = n.Content,
                    PublishDate = n.PublishDate,
                    OriginalUrl = n.OriginalUrl,
                    SourceId = n.SourceId,
                })
                .ToListAsync(cancellationToken);

            return result;
        }

        public async Task<ArticleDto?> GetByIdAsync(Guid id, CancellationToken token)
        {
            var result = await mediator.Send(new GetArticleById(id), token);
            return result;
        }

        public async Task CreateNews(ArticleDto article, CancellationToken cancellationToken)
        {
            await mediator.Send(new InsertArticleCommand { Article = article }, cancellationToken);
        }

        public async Task UpdatePartialArticleAsync(Guid id, string? updatedArticleTitle, decimal? updatedArticleRate, CancellationToken cancellationToken)
        {
            var command = new UpdatePartialArticleCommand(id: id, updatedArticleRate: updatedArticleRate,  updatedArticleTitle: updatedArticleTitle);
            await mediator.Send(command, cancellationToken);
        }

        public async Task<IReadOnlyCollection<ArticleDto>> GetNewsByRateAndSource(decimal? minRate, int? sourceId, CancellationToken cancellationToken)
        {
            return await mediator.Send(new GetArticleByRateAndSourceQuery(minRate, sourceId), cancellationToken);
        }

        public async Task AggregateNews(CancellationToken cancellationToken)
        {
            // 1. Получаем RSS-источники
            var rssUrls = await dbContext.Sources
                .Where(s => !string.IsNullOrWhiteSpace(s.RssUrl))
                .Select(s => new { s.Id, s.RssUrl })
                .ToArrayAsync(cancellationToken);

            if (rssUrls.Length == 0)
            {
                Log.Information("No RSS sources configured");
                return;
            }

            // 2. Загружаем существующие URL одним запросом
            var existingUrls = await dbContext.Articles
                .Select(a => a.OriginalUrl)
                .ToHashSetAsync(cancellationToken);

            // 3. Читаем RSS-ленты с ограничением параллелизма
            var articlesFromSources = new List<RssNewsInfoDto>();
            using var rssSemaphore = new SemaphoreSlim(3); // Не более 3 одновременных RSS-запросов

            var rssTasks = rssUrls.Select(async source =>
            {
                await rssSemaphore.WaitAsync(cancellationToken);
                try
                {
                    var articleRssData = await rssReader.ReadFeedAsync(source.RssUrl, source.Id, cancellationToken);

                    // Фильтруем только новые URL
                    var newArticles = articleRssData
                        .Where(a => !existingUrls.Contains(a.OriginalUrl))
                        .ToList();

                    lock (articlesFromSources)
                    {
                        articlesFromSources.AddRange(newArticles);
                    }

                    Log.Information("RSS source {SourceId}: {Total} items, {New} new",
                        source.Id, articleRssData.Count, newArticles.Count);
                }
                catch (OperationCanceledException)
                {
                    // Не логируем отмену
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Error reading RSS feed from source {SourceId} ({Url})", source.Id, source.RssUrl);
                }
                finally
                {
                    rssSemaphore.Release();
                }
            });

            await Task.WhenAll(rssTasks);

            if (articlesFromSources.Count == 0)
            {
                Log.Information("No new articles found in RSS feeds");
                return;
            }

            // 4. Парсим HTML-страницы
            var (articles, errorCount) = await webScraper.ScrapeArticlesAsync(
                articlesFromSources,
                maxConcurrency: 5,
                cancellationToken);

            if (errorCount > 0)
            {
                Log.Warning("Web scraping completed with {ErrorCount} errors out of {Total} articles",
                    errorCount, articlesFromSources.Count);
            }

            // 5. Сохраняем в БД
            await InsertParsedNewsAsync(articles, cancellationToken);
        }

        private async Task InsertParsedNewsAsync(IEnumerable<ArticleDto> news, CancellationToken token)
        {
            var newsList = news as IReadOnlyCollection<ArticleDto> ?? news.ToList();

            if (newsList.Count == 0)
                return;

            Log.Information("Начало обработки {Count} статей", newsList.Count);

            using var transaction = await dbContext.Database
                .BeginTransactionAsync(System.Data.IsolationLevel.Serializable, token);

            try
            {
                // 1. Проверяем дубликаты среди тех, что могли появиться между нашими запросами
                var incomingUrls = newsList.Select(a => a.OriginalUrl).ToHashSet();

                var existingUrls = await dbContext.Articles
                    .Where(a => incomingUrls.Contains(a.OriginalUrl))
                    .Select(a => a.OriginalUrl)
                    .ToHashSetAsync(token);

                // 2. Фильтруем только новые статьи
                var newArticles = newsList
                    .Where(article => !existingUrls.Contains(article.OriginalUrl))
                    .ToList();

                var duplicateCount = newsList.Count - newArticles.Count;

                if (duplicateCount > 0)
                {
                    Log.Information("Пропущено {DuplicateCount} дубликатов", duplicateCount);
                }

                Log.Information("Найдено {DuplicateCount} дубликатов, {NewCount} новых статей",
                    duplicateCount, newArticles.Count);

                // 3. Вставляем только новые статьи
                if (newArticles.Count > 0)
                {
                    await mediator.Send(new InsertArticleDataCommand { Articles = newArticles }, token);
                    await transaction.CommitAsync(token);
                    Log.Information("Успешно сохранено {Count} новых статей", newArticles.Count);
                }
                else
                {
                    await transaction.CommitAsync(token);
                    Log.Information("Нет новых статей для сохранения");
                }
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(token);
                Log.Error(ex, "Ошибка при сохранении статей");
                throw;
            }
        }
    }
}
