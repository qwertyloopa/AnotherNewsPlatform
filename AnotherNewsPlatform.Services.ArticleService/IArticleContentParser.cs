using HtmlAgilityPack;

namespace AnotherNewsPlatform.Services.NewsService;

public interface IArticleContentParser
{
    /// <summary>
    /// Определяет, может ли этот парсер обработать указанный источник.
    /// </summary>
    bool CanParse(long sourceId);

    /// <summary>
    /// Извлекает текст статьи из HTML-документа.
    /// </summary>
    string Parse(HtmlDocument doc);
}