using AnotherNewsPlatform.CQS.Articles.Commands;
using AnotherNewsPlatform.CQS.Articles.Query;
using AnotherNewsPlatform.Database;
using AnotherNewsPlatform.Services.NewsService;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace AnotherNewsPlatform.WebApi.Infrastructure;

/// <summary>
/// Содержит методы для рекуррентных задач Hangfire.
/// Все методы должны быть public virtual (Hangfire требует virtual для повторных попыток).
/// </summary>
public class HangfireJobs
{
    private readonly IServiceProvider _serviceProvider;

    public HangfireJobs(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    /// <summary>
    /// Периодическая агрегация новостей из RSS-источников.
    /// </summary>
    public async Task AggregateNewsJob(CancellationToken cancellationToken = default)
    {
        Log.Information("Hangfire: AggregateNewsJob started");

        try
        {
            using var scope = _serviceProvider.CreateScope();
            var aggregateNewsService = scope.ServiceProvider.GetRequiredService<IAggregateNewsService>();

            await aggregateNewsService.AggregateNews(cancellationToken);

            Log.Information("Hangfire: AggregateNewsJob completed successfully");
        }
        catch (OperationCanceledException)
        {
            Log.Warning("Hangfire: AggregateNewsJob was cancelled");
            throw;
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Hangfire: AggregateNewsJob failed");
            throw;
        }
    }

    /// <summary>
    /// Периодическая оценка тональности непрорешённых статей через Ollama.
    /// </summary>
    public async Task RateUnratedNewsJob(CancellationToken cancellationToken = default)
    {
        Log.Information("Hangfire: RateUnratedNewsJob started");

        try
        {
            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AnpDbContext>();
            var aggregateNewsService = scope.ServiceProvider.GetRequiredService<IAggregateNewsService>();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            // Получаем статьи с Rate == 0 (ещё не оценённые)
            var unratedArticles = await dbContext.Articles
                .AsNoTracking()
                .Where(a => a.Rate == 0)
                .Select(a => a.Id)
                .ToArrayAsync(cancellationToken);

            if (unratedArticles.Length == 0)
            {
                Log.Information("Hangfire: RateUnratedNewsJob — no unrated articles found");
                return;
            }

            Log.Information("Hangfire: RateUnratedNewsJob — found {Count} unrated articles", unratedArticles.Length);

            var ratedCount = 0;
            var errorCount = 0;

            foreach (var articleId in unratedArticles)
            {
                cancellationToken.ThrowIfCancellationRequested();

                try
                {
                    var rate = await aggregateNewsService.RateNewsAsync(articleId, cancellationToken);
                    await mediator.Send(new InsertRateToArticleCommand(articleId, rate), cancellationToken);
                    ratedCount++;
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Hangfire: RateUnratedNewsJob — failed to rate article {ArticleId}", articleId);
                    errorCount++;
                }
            }

            Log.Information(
                "Hangfire: RateUnratedNewsJob completed — rated {RatedCount}, errors {ErrorCount}",
                ratedCount, errorCount);
        }
        catch (OperationCanceledException)
        {
            Log.Warning("Hangfire: RateUnratedNewsJob was cancelled");
            throw;
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Hangfire: RateUnratedNewsJob failed");
            throw;
        }
    }
}