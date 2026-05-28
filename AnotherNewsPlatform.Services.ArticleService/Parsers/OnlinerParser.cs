using HtmlAgilityPack;
using Serilog;

namespace AnotherNewsPlatform.Services.NewsService;

public sealed class OnlinerParser : IArticleContentParser
{
    public bool CanParse(long sourceId) => sourceId == 1;

    public string Parse(HtmlDocument doc)
    {
        try
        {
            var article = doc.DocumentNode.SelectSingleNode("//div[@class='news-text']");
            if (article is null)
            {
                Log.Warning("OnlinerParser: news-text div not found");
                return string.Empty;
            }

            const string scriptRegex = @"<script.*?>.*?</script>";
            var result = System.Text.RegularExpressions.Regex.Replace(
                article.InnerText, scriptRegex, string.Empty,
                System.Text.RegularExpressions.RegexOptions.Singleline).Trim();

            return result;
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error parsing data from Onliner");
            return string.Empty;
        }
    }
}