using HtmlAgilityPack;
using Serilog;

namespace AnotherNewsPlatform.Services.NewsService;

public sealed class LentaParser : IArticleContentParser
{
    public bool CanParse(long sourceId) => sourceId == 3;

    public string Parse(HtmlDocument doc)
    {
        try
        {
            var article = doc.DocumentNode.SelectSingleNode("//div[@class='b-topic__content']");
            if (article is null)
            {
                Log.Warning("LentaParser: b-topic__content div not found");
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
            Log.Error(ex, "Error parsing data from lenta.ru");
            return string.Empty;
        }
    }
}