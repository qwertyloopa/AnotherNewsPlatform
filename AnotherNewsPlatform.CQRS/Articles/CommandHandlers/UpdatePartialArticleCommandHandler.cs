using AnotherNewsPlatform.CQS.Articles.Commands;
using AnotherNewsPlatform.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AnotherNewsPlatform.CQS.Articles.CommandHandlers;

public class UpdatePartialArticleCommandHandler(AnpDbContext dbContext) : IRequestHandler<UpdatePartialArticleCommand>
{
    public async Task Handle(UpdatePartialArticleCommand request, CancellationToken cancellationToken)
    {
        var articleToChange = await dbContext.Articles.SingleOrDefaultAsync(a => a.Id == request.id);
        if (articleToChange != null)
        {
            if (request.updatedArticleRate != null)
            {
                articleToChange.Title = request.updatedArticleTitle;
            }

            if (request.updatedArticleRate != null)
            {
                articleToChange.Rate = (decimal)request.updatedArticleRate;
            }
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}