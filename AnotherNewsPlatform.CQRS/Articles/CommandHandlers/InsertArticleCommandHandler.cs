using AnotherNewsPlatform.Core.Mappers;
using AnotherNewsPlatform.CQS.Articles.Commands;
using AnotherNewsPlatform.Database;
using MediatR;

namespace AnotherNewsPlatform.CQS.Articles.CommandHandlers;

public class InsertArticleCommandHandler(AnpDbContext dbContext, ArticleMapper mapper) : IRequestHandler<InsertArticleCommand>
{
    public async Task Handle(InsertArticleCommand request, CancellationToken cancellationToken)
    {
        var articleToAdd = mapper.ToEntity(request.Article);
        await dbContext.Articles.AddAsync(articleToAdd, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
