using AnotherNewsPlatform.Core.DTOs;

namespace AnotherNewsPlatform.Services.NewsService;

public interface IWebScraper
{
    /// <summary>
    /// Парсит HTML-страницы статей и извлекает полный текст.
    /// </summary>
    /// <param name="articles">Список статей из RSS для парсинга.</param>
    /// <param name="maxConcurrency">Максимальное количество одновременных запросов.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Кортеж: список обработанных статей и количество ошибок.</returns>
    Task<(IReadOnlyCollection<ArticleDto> Articles, int ErrorCount)> ScrapeArticlesAsync(
        IEnumerable<RssNewsInfoDto> articles,
        int maxConcurrency,
        CancellationToken cancellationToken);
}