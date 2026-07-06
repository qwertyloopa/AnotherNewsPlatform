namespace AnotherNewsPlatform.Services.NewsService
{
    public interface IAggregateNewsService
    {
        Task AggregateNews(CancellationToken cancellationToken);
        Task<decimal> RateNewsAsync(Guid id, CancellationToken cancellationToken);
    }
}