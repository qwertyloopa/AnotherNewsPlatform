using AnotherNewsPlatform.Core.DTOs;

namespace AnotherNewsPlatform.Services.NewsService
{
    public interface INewsService
    {
        Task<List<ArticleDto>> GetNewsAsync(CancellationToken cancellationToken);
        Task<ArticleDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task CreateNews(ArticleDto article, CancellationToken cancellationToken);
        Task AggregateNews(CancellationToken cancellationToken);
        Task<IReadOnlyCollection<ArticleDto>> GetNewsByRateAndSource(decimal? minRate, int? sourceId, CancellationToken cancellationToken);

        Task UpdatePartialArticleAsync(Guid id, string? updatedArticleTitle, decimal? updatedArticleRate,
            CancellationToken cancellationToken);
    }
}
