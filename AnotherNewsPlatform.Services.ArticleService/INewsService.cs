using AnotherNewsPlatform.Core.DTOs;

namespace AnotherNewsPlatform.Services.NewsService
{
    public interface INewsService
    {

        Task<ArticleDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<ArticleDto>> GetNewsByPage(int pageNumber, int pageSize);
        Task CreateNews(ArticleDto article, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<ArticleDto>> GetNewsByRateAndSource(decimal? minRate, int? sourceId, CancellationToken cancellationToken);

        Task UpdatePartialArticleAsync(Guid id, string? updatedArticleTitle, decimal? updatedArticleRate,
            CancellationToken cancellationToken);
        //Task<int> RateNewsAsync(Guid id, CancellationToken cancellationToken);
        Task<int> GetNewsCountAsync(CancellationToken cancellationToken);
    }
}
