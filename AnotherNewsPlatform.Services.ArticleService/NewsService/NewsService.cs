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

        public async Task<NewsDto[]> GetNewsAsync()
        {
            var result = await _dbContext.News
            .AsNoTracking()
            .Include(n => n.Author)
            .Include(n => n.Source)
            .Include(n => n.Category)
            .Include(n => n.Comments)
            .OrderByDescending(n => n.PublishDate)
            .Select(n => new NewsDto
            {
                Id = n.Id,
                Title = n.Title,
                Content = n.Content,
                Text = n.Text,
                PublishDate = n.PublishDate,
                AuthorId = n.AuthorId,
                //AuthorName = n.Author.Name,
                SourceId = n.SourceId,
                //SourceName = n.Source.Name,
                CategoryId = n.CategoryId,
                //CategoryName = n.Category.Name,
                Comments = n.Comments.Select(c => c.Text).ToList()
            })
            .ToArrayAsync();
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
                Text = n.Text,
                PublishDate = n.PublishDate,
                AuthorId = n.AuthorId,
                //AuthorName = n.Author.Name,
                SourceId = n.SourceId,
                //SourceName = n.Source.Name,
                CategoryId = n.CategoryId,
                //CategoryName = n.Category.Name,
                Comments = n.Comments.Select(c => c.Text).ToList()
            })
            .FirstOrDefaultAsync();
            return result;
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
