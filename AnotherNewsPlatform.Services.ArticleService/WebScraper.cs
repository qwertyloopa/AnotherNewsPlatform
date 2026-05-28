using System.Collections.Concurrent;
using AnotherNewsPlatform.Core.DTOs;
using HtmlAgilityPack;
using Serilog;

namespace AnotherNewsPlatform.Services.NewsService;

public sealed class WebScraper : IWebScraper
{
    private readonly IEnumerable<IArticleContentParser> _parsers;
    private readonly HttpClient _httpClient;

    public WebScraper(IEnumerable<IArticleContentParser> parsers, HttpClient httpClient)
    {
        _parsers = parsers;
        _httpClient = httpClient;
    }

    public async Task<(IReadOnlyCollection<ArticleDto> Articles, int ErrorCount)> ScrapeArticlesAsync(
        IEnumerable<RssNewsInfoDto> articles,
        int maxConcurrency,
        CancellationToken cancellationToken)
    {
        var articleList = new ConcurrentBag<ArticleDto>();
        var errorCount = 0;

        using var semaphore = new SemaphoreSlim(maxConcurrency);

        var tasks = articles.Select(async rssArticleInfo =>
        {
            await semaphore.WaitAsync(cancellationToken);

            try
            {
                var doc = await FetchHtmlDocumentAsync(rssArticleInfo.OriginalUrl, cancellationToken);

                var articleDto = new ArticleDto
                {
                    Title = rssArticleInfo.Title,
                    Content = rssArticleInfo.Content,
                    Text = ExtractText(rssArticleInfo, doc),
                    OriginalUrl = rssArticleInfo.OriginalUrl,
                    PublishDate = rssArticleInfo.PublishDate,
                    SourceId = rssArticleInfo.SourceId,
                };

                articleList.Add(articleDto);
            }
            catch (OperationCanceledException)
            {
                // Не логируем отмену как ошибку
            }
            catch (Exception ex)
            {
                Interlocked.Increment(ref errorCount);
                Log.Error(ex, "Error scraping article from {Url}", rssArticleInfo.OriginalUrl);
            }
            finally
            {
                semaphore.Release();
            }
        });

        await Task.WhenAll(tasks);

        return (articleList.ToList().AsReadOnly(), errorCount);
    }

    private async Task<HtmlDocument> FetchHtmlDocumentAsync(string url, CancellationToken cancellationToken)
    {
        var html = await _httpClient.GetStringAsync(url, cancellationToken);

        var doc = new HtmlDocument();
        doc.LoadHtml(html);

        return doc;
    }

    private string ExtractText(RssNewsInfoDto rss, HtmlDocument doc)
    {
        var parser = _parsers.FirstOrDefault(p => p.CanParse(rss.SourceId));

        if (parser is not null)
        {
            return parser.Parse(doc);
        }

        return $"Sorry, but now we can't parse data from this source. You need to go for this link {rss.OriginalUrl} to read the article.";
    }
}