using AnotherNewsPlatform.Core.Mappers;
using AnotherNewsPlatform.CQS.Articles.Commands;
using AnotherNewsPlatform.Database;
using AnotherNewsPlatform.Database.Entities;
using MediatR;

namespace AnotherNewsPlatform.CQS.Articles.CommandHandlers
{
    internal class InsertArticleDataCommandHandler(AnpDbContext dbContext) : IRequestHandler<InsertArticleDataCommand>
    {
        public async Task Handle(InsertArticleDataCommand request, CancellationToken token)
        {
            List<Article> articleEntities = new List<Article>();
            var mapper = new ArticleMapper();
            foreach (var article in request.Articles)
            {
                var articleEntity = mapper.ToEntity(article);
                articleEntities.Add(articleEntity);
            }
            await dbContext.Articles.AddRangeAsync(articleEntities, token);
            await dbContext.SaveChangesAsync(token);
        }
    }
}
