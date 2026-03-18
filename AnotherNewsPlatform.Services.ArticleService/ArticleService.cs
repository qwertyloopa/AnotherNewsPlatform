using AnotherNewsPlatform.DataAccess;
using AnotherNewsPlatform.DataAccess.Entities;

namespace AnotherNewsPlatform.Services.NewsService
{
    public class ArticleService(DbContext dbContext) : IArticleService
    {
        private readonly DbContext _dbContext = dbContext;
        public News[] GetNews()
        {
            return _dbContext.News.ToArray();
        }
        
        public void AggregateNews()
        {

        }

        //public Task<object> GetDataFromRssAsync() { }

    }
}
