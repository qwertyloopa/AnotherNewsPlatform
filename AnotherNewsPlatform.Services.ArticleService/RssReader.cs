using System.Collections.Concurrent;
using System.ServiceModel.Syndication;
using System.Xml;
using AnotherNewsPlatform.Core.DTOs;
using Serilog;

namespace AnotherNewsPlatform.Services.NewsService;

public sealed class RssReader : IRssReader
{
    public async Task<IReadOnlyCollection<RssNewsInfoDto>> ReadFeedAsync(string rssUrl, long sourceId, CancellationToken cancellationToken)
    {
        using var reader = XmlReader.Create(rssUrl, new XmlReaderSettings
        {
            Async = true,
            DtdProcessing = DtdProcessing.Parse,
        });

        var feed = SyndicationFeed.Load(reader);
        var articles = new List<RssNewsInfoDto>(feed.Items.Count());

        foreach (var item in feed.Items)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var articleData = new RssNewsInfoDto
            {
                Title = item.Title?.Text ?? string.Empty,
                Content = item.Summary?.Text ?? string.Empty,
                SourceId = sourceId,
                OriginalUrl = item.Id ?? item.Links.FirstOrDefault()?.Uri.ToString() ?? string.Empty,
                PublishDate = item.PublishDate.UtcDateTime,
            };

            articles.Add(articleData);
        }

        return articles.AsReadOnly();
    }
}