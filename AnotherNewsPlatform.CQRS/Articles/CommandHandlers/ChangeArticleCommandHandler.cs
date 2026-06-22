using AnotherNewsPlatform.Core.Mappers;
using AnotherNewsPlatform.CQS.Articles.Commands;
using AnotherNewsPlatform.Database;
using MediatR;

namespace AnotherNewsPlatform.CQS.Articles.CommandHandlers;

public class ChangeArticleCommandHandler(AnpDbContext dbContext): IRequestHandler<ChangeArticleCommand>
{
    public async Task Handle(ChangeArticleCommand request, CancellationToken cancellationToken)
    {
        var articleToUpdate = dbContext.Articles.FirstOrDefault(x => x.Id == request.article.Id);
        if (articleToUpdate == null) return;
        var article = new ArticleMapper().ToEntity(request.article);
        articleToUpdate = article;
        dbContext.Articles.Update(articleToUpdate);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}