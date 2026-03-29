using AnotherNewsPlatform.DataAccess;
using AnotherNewsPlatform.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;


namespace AnotherNewsPlatform.Services.NewsService
{
    public class NewsService: INewsService
    {
        private readonly AnpDbContext _dbContext;

        public NewsService(AnpDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<News[]> GetNewsAsync()
        {
            //var query = _dbContext.FindAsync
            return await _dbContext.News.ToArrayAsync();
        }

        public void AggregateNews()
        {

        }

        //public Task<object> GetDataFromRssAsync() 
        //{

            //return ;
        //}

    }
}
