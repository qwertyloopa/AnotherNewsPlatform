using AnotherNewsPlatform.Core.DTOs;
using AnotherNewsPlatform.DataAccess;
using AnotherNewsPlatform.DataAccess.Entities;
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
    public class NewsService: INewsService
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
                    //AuthorId = n.AuthorId,
                    //AuthorName = n.Author.Name,
                    SourceId = n.SourceId,
                    SourceName = n.Source.Name,
                    //CategoryId = n.CategoryId,
                    //CategoryName = n.Category.Name,
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

            foreach (var article in articlesFromSources) 
            {
                
            }

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
           var articleList = new List<NewsDto>();
            foreach (var article in articleToParse)
            {
                var web = new HtmlWeb();
                var doc = await web.LoadFromWebAsync(article.OriginalUrl);

                var articleDto = new NewsDto()
                {
                    Title = article.Title,
                    Content = ParseOnlinerData(doc),
                    PublishDate = article.PublishDate,
                    SourceId = article.SourceId,
                };

                articleList.Add(articleDto);
            }

            return articleList.AsReadOnly();
        }

        private string ParseOnlinerData(HtmlDocument doc)
        {
            try
            {
                var article = doc.DocumentNode.SelectSingleNode(".//div[@class='news-text']");

                var unusedContent = doc.DocumentNode.SelectSingleNode(".//div[@class = 'ad']");
                if (unusedContent != null) doc.DocumentNode.RemoveChild(unusedContent);
                var unusedContent2 = doc.DocumentNode.SelectSingleNode(".//div[@class = 'ad-block']");
                if (unusedContent2 != null) doc.DocumentNode.RemoveChild(unusedContent2);


                string scriptRegex = @"<script.*?>.*?</script>";
                var result = Regex.Replace(article.InnerText, scriptRegex, string.Empty);
                return article.InnerText;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error parsing onliner data");
                return string.Empty;
            }
        }
    }
}
