using AnotherNewsPlatform.Core.DTOs;
using AnotherNewsPlatform.Database;
using AnotherNewsPlatform.Database.Entities;
using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.ServiceModel.Syndication;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;


namespace AnotherNewsPlatform.NewsService
{
    
    public class NewsService : INewsService
    {
        private readonly AnpDbContext _dbContext;

        public NewsService(AnpDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<NewsDto>> GetNewsAsync()
        {
            var result = await _dbContext.News
            .AsNoTracking()
            .Include(n => n.Source)
            //.Include(n => n.Category)
            .Include(n => n.Comments)
            .OrderByDescending(n => n.PublishDate)
            .Select(n => new NewsDto
            {
                Id = n.Id,
                Title = n.Title,
                Content = n.Content,
                PublishDate = n.PublishDate,
                //AuthorId = n.AuthorId,
                //AuthorName = n.Author.Name,
                SourceId = n.SourceId,
                SourceName = n.Source.Name,
                //CategoryId = n.CategoryId,
                //CategoryName = n.Category.Name,
                Comments = n.Comments.Select(c => c.Text).ToList()
            })
            .ToListAsync();
            return result;
        }

        public async Task<NewsDto?> GetByIdAsync(Guid id)
        {
            var result = await _dbContext.News
                .AsNoTracking()
                .Where(n => n.Id == id)
                .Select(n => new NewsDto
                {
                    Id = n.Id,
                    Title = n.Title,
                    Content = n.Content,
                    PublishDate = n.PublishDate,
                    SourceId = n.SourceId,
                    SourceName = n.Source.Name,
                    Comments = n.Comments.Select(c => c.Text).ToList()
                })
                .FirstOrDefaultAsync();
            return result;
        }
        public async Task AggregateNews(CancellationToken cancellationToken)
        {
            //1. Get data from RSS
            //2. Web scrapping
            //3. Writing to database

            var rssUrls = (await _dbContext.Source
                .Where(s => !string.IsNullOrWhiteSpace(s.RssUrl))
                .Select(s => new Tuple<long, string>(s.Id, s.RssUrl))
                .ToArrayAsync(cancellationToken))
               .AsReadOnly();

            var articlesFromSources = new List<RssNewsInfoDto>();

            foreach (var rssUrlTuple in rssUrls)
            {
                var articleRssData = GetDataFromRssAsync(rssUrlTuple.Item2, rssUrlTuple.Item1, cancellationToken);
                articlesFromSources.AddRange(articleRssData);
            }

            var articles = await WebScrapNewsText(articlesFromSources);
            
            await InsertParsedNewsAsync(articles, cancellationToken);
        }
        private IEnumerable<RssNewsInfoDto> GetDataFromRssAsync(string url, long id, CancellationToken cancellationToken)
        {
            using (var reader = XmlReader.Create(url))
            {
                var feed = SyndicationFeed.Load(reader);
                var articles = new ConcurrentBag<RssNewsInfoDto>();

                Parallel.ForEach(feed.Items, item => 
                {
                    var articleData = new RssNewsInfoDto()
                    {
                        Title = item.Title.Text,
                        Content = item.Summary.Text,
                        SourceId = id,
                        OriginalUrl = item.Id,
                        PublishDate = item.PublishDate.UtcDateTime,
                    };
                    articles.Add(articleData);
                });

                return articles;
            }
        }

        private async Task<ReadOnlyCollection<NewsDto>> WebScrapNewsText(IEnumerable<RssNewsInfoDto> articleToParse)
        {
            var articleList = new ConcurrentBag<NewsDto>();
            var parallelOptions = new ParallelOptions
            {
                MaxDegreeOfParallelism = 5 // Set the maximum degree of parallelism (to don't exceed the number of CPU cores)
            };

            await Parallel.ForEachAsync(
                articleToParse,
                async (rssArticleInfo, cancellationToken) =>
                {
                    try
                    {
                        var web = new HtmlWeb();
                        var doc = await web.LoadFromWebAsync(rssArticleInfo.OriginalUrl);

                        var articleDto = new NewsDto()
                        {
                            Title = rssArticleInfo.Title,
                            Content = rssArticleInfo.Content,
                            Text = ParseOnlinerData(doc),
                            PublishDate = rssArticleInfo.PublishDate,
                            SourceId = rssArticleInfo.SourceId,
                        };

                        articleList.Add(articleDto);
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex, $"Error scraping article from {rssArticleInfo.OriginalUrl}");
                        // Continue scraping other articles
                    }
                });

            return articleList.ToList().AsReadOnly();
        }

        private string ParseOnlinerData(HtmlDocument doc)
        {
            try
            {
                var article = doc.DocumentNode.SelectSingleNode(".//div[@class='news-text']");

                var unusedContent = doc.DocumentNode.SelectSingleNode(".//div[@class = 'ad']");
                if (unusedContent != null) doc.DocumentNode.RemoveChild(unusedContent);
                var unusedContent2 = doc.DocumentNode.SelectSingleNode(".//div[@class = 'news-reference']");
                if (unusedContent2 != null) doc.DocumentNode.RemoveChild(unusedContent2);
                var unusedContent3 = doc.DocumentNode.SelectSingleNode(".//div[@class = 'news-widget]");
                if (unusedContent3 != null) doc.DocumentNode.RemoveChild(unusedContent3);
                var unusedContent4 = doc.DocumentNode.SelectSingleNode(".//p[last()]");
                if (unusedContent4 != null) doc.DocumentNode.RemoveChild(unusedContent4);



                const string scriptRegex = "/<script.*?>.*?</script>";
                var result = Regex.Replace(article.InnerText, scriptRegex, string.Empty).Trim();
                return result;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error parsing data from Onliner");
                return string.Empty;
            }
        }

        private string ParseBTRCData(HtmlDocument doc)
        {
            try
            {
                var article = doc.DocumentNode.SelectSingleNode(".//div[@class='news-text']");

                var unusedContent = doc.DocumentNode.SelectSingleNode(".//div[@class = 'ad']");
                if (unusedContent != null) doc.DocumentNode.RemoveChild(unusedContent);
                var unusedContent2 = doc.DocumentNode.SelectSingleNode(".//div[@class = 'news-reference']");
                if (unusedContent2 != null) doc.DocumentNode.RemoveChild(unusedContent2);
                var unusedContent3 = doc.DocumentNode.SelectSingleNode(".//div[@class = 'news-widget]");
                if (unusedContent3 != null) doc.DocumentNode.RemoveChild(unusedContent3);
                var unusedContent4 = doc.DocumentNode.SelectSingleNode(".//p[last()]");
                if (unusedContent4 != null) doc.DocumentNode.RemoveChild(unusedContent4);



                const string scriptRegex = "/<script.*?>.*?</script>";
                var result = Regex.Replace(article.InnerText, scriptRegex, string.Empty).Trim();
                return result;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error parsing data from БТ");
                return string.Empty;
            }
        }

        private async Task InsertParsedNewsAsync(IEnumerable<NewsDto> news, CancellationToken token)
        {
            var articleEntities = news.Select(n => new News
            {
                Id = n.Id,
                Title = n.Title,
                Content = n.Content,
                Text = n.Text,
                PublishDate = n.PublishDate,
                SourceId = n.SourceId,
            });
            await _dbContext.News.AddRangeAsync(articleEntities, token);
            await _dbContext.SaveChangesAsync(token);
        }
    }
}

