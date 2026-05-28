using AnotherNewsPlatform.Core.DTOs;

namespace AnotherNewsPlatform.Services.NewsService;

public interface IRssReader
{
    Task<IReadOnlyCollection<RssNewsInfoDto>> ReadFeedAsync(string rssUrl, long sourceId, CancellationToken cancellationToken);
}