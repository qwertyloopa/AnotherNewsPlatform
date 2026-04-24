using AnotherNewsPlatform.Core.DTOs;
using AnotherNewsPlatform.Database;
using AnotherNewsPlatform.Database.Entities;
using HtmlAgilityPack;
using Serilog;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.ServiceModel.Syndication;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;


namespace AnotherNewsPlatform.Services.NewsService
{
    
    public class NewsService : INewsService
    {
        private readonly AnpDbContext _dbContext;

        public NewsService(AnpDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ArticleDto>> GetNewsAsync()
        {
            var result = await _dbContext.Articles
            .AsNoTracking()
            .Include(n => n.Source)
            //.Include(n => n.Category)
            .Include(n => n.Comments)
            .OrderByDescending(n => n.PublishDate)
            .Select(n => new ArticleDto
            {
                Id = n.Id,
                Title = n.Title,
                Content = n.Content,
                PublishDate = n.PublishDate,
                OriginalUrl = n.OriginalUrl,
                SourceId = n.SourceId,
                //CategoryId = n.CategoryId,
                //CategoryName = n.Category.Name,
                //Comments = n.Comments.Select(c => c.Text).ToList()
            })
            .ToListAsync();
            return result;
        }

        public async Task<ArticleDto?> GetByIdAsync(Guid id)
        {
            var result = await _dbContext.Articles
                .AsNoTracking()
                .Where(n => n.Id == id)
                .Select(n => new ArticleDto
                {
                    Id = n.Id,
                    Title = n.Title,
                    Content = n.Content,
                    PublishDate = n.PublishDate,
                    OriginalUrl = n.OriginalUrl,
                    SourceId = n.SourceId,
                    //Comments = n.Comments.Select(c => c.Text).ToList()
                })
                .FirstOrDefaultAsync();
            return result;
        }
        public async Task AggregateNews(CancellationToken cancellationToken)
        {
            //1. Get data from RSS
            //2. Web scrapping
            //3. Writing to database

            var rssUrls = (await _dbContext.Sources
                .Where(s => !string.IsNullOrWhiteSpace(s.RssUrl))
                .Select(s => new Tuple<long, string>(s.Id, s.RssUrl))
                .ToArrayAsync(cancellationToken))
               .AsReadOnly();

            var existingUrls= await GetExsistingNews(cancellationToken);

            var newRssUrls = rssUrls.Where(rssUrlTuple => !existingUrls.Contains(rssUrlTuple.Item2))
            .ToArray()
            .AsReadOnly();

            var articlesFromSources = new List<RssNewsInfoDto>();

            //var urlsToProcess = newRssUrls.Count > 0 ? newRssUrls : rssUrls;
            foreach (var rssUrlTuple in newRssUrls)
            {
                var articleRssData = await GetDataFromRss(rssUrlTuple.Item2, rssUrlTuple.Item1, cancellationToken);
                articlesFromSources.AddRange(articleRssData);
            }

            

            var articles = await WebScrapNewsText(articlesFromSources);
            
            await InsertParsedNewsAsync(articles, cancellationToken);
        }

        async Task<ReadOnlyCollection<string>> GetExsistingNews(CancellationToken cancellationToken)
        {
            return (await _dbContext.Articles.Select(n => n.OriginalUrl).ToArrayAsync(cancellationToken)).AsReadOnly();
        }
        private async Task<IEnumerable<RssNewsInfoDto>> GetDataFromRss(string url, long id, CancellationToken cancellationToken)
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

        private async Task<ReadOnlyCollection<ArticleDto>> WebScrapNewsText(IEnumerable<RssNewsInfoDto> articleToParse)
        {
            var articleList = new ConcurrentBag<ArticleDto>();
            

            await Parallel.ForEachAsync(
                articleToParse,
                async (rssArticleInfo, cancellationToken) =>
                {
                    try
                    {
                        var web = new HtmlWeb();
                        var doc = await web.LoadFromWebAsync(rssArticleInfo.OriginalUrl);

                        var articleDto = new ArticleDto()
                        {
                            Title = rssArticleInfo.Title,
                            Content = rssArticleInfo.Content,
                            Text = ScrapSelector(rssArticleInfo, doc),
                            OriginalUrl = rssArticleInfo.OriginalUrl,
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

        private string ParseBTData(HtmlDocument doc)
        {
            try
            {
                var article = doc.DocumentNode.SelectSingleNode(".//div[@class='flex flex-col gap-10 overflow-visible h-full']");

                var unusedContent = doc.DocumentNode.SelectSingleNode(".//img");
                if (unusedContent != null) doc.DocumentNode.RemoveChild(unusedContent);
                /*var unusedContent2 = doc.DocumentNode.SelectSingleNode(".//div[@class = 'news-reference']");
                if (unusedContent2 != null) doc.DocumentNode.RemoveChild(unusedContent2);
                var unusedContent3 = doc.DocumentNode.SelectSingleNode(".//div[@class = 'news-widget]");
                if (unusedContent3 != null) doc.DocumentNode.RemoveChild(unusedContent3);
                var unusedContent4 = doc.DocumentNode.SelectSingleNode(".//p[last()]");
                if (unusedContent4 != null) doc.DocumentNode.RemoveChild(unusedContent4);*/



                const string scriptRegex = "/<script.*?>.*?</script>";
                var result = Regex.Replace(article.InnerText, scriptRegex, string.Empty).Trim();
                return result;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error parsing data from sb.by");
                return string.Empty;
            }
        }

        private string ParseLentaData(HtmlDocument doc)
        {
            try
            {
                var article = doc.DocumentNode.SelectSingleNode(".//div[@class='b-topic__content']");

                /*var unusedContent = doc.DocumentNode.SelectSingleNode(".//div[@class = 'ad']");
                if (unusedContent != null) doc.DocumentNode.RemoveChild(unusedContent);
                var unusedContent2 = doc.DocumentNode.SelectSingleNode(".//div[@class = 'news-reference']");
                if (unusedContent2 != null) doc.DocumentNode.RemoveChild(unusedContent2);
                var unusedContent3 = doc.DocumentNode.SelectSingleNode(".//div[@class = 'news-widget]");
                if (unusedContent3 != null) doc.DocumentNode.RemoveChild(unusedContent3);
                var unusedContent4 = doc.DocumentNode.SelectSingleNode(".//p[last()]");
                if (unusedContent4 != null) doc.DocumentNode.RemoveChild(unusedContent4);*/

                const string scriptRegex = "/<script.*?>.*?</script>";
                var result = Regex.Replace(article.InnerText, scriptRegex, string.Empty).Trim();
                return result;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error parsing data from lenta.ru");
                return string.Empty;
            }
        }

        private string ScrapSelector(RssNewsInfoDto rss, HtmlDocument doc) //костылики
        {
            switch ((int)rss.SourceId)
            {
                case 1:
                    return ParseOnlinerData(doc);
                case 2:
                    return ParseBTData(doc);
                default:
                    return $"Sorry, but now we can't parse data from this source. You need to go for this link {rss.OriginalUrl} to read the article.";
            }
        }

        private async Task InsertParsedNewsAsync(IEnumerable<ArticleDto> news, CancellationToken token)
        {
            var articleEntities = news.Select(n => new Article
            {
                Title = n.Title,
                Content = n.Content,
                Text = n.Text,
                OriginalUrl = n.OriginalUrl,
                PublishDate = n.PublishDate,
                SourceId = n.SourceId,
            });
            await _dbContext.Articles.AddRangeAsync(articleEntities, token);
            await _dbContext.SaveChangesAsync(token);
        }
    }
}

