using AnotherNewsPlatform.CQS.Articles.Commands;
using AnotherNewsPlatform.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnotherNewsPlatform.CQS.Articles.CommandHandlers
{
    public class InsertRateToArticleCommandHandler(AnpDbContext dbContext) : IRequestHandler<InsertRateToArticleCommand>
    {
        public async Task Handle(InsertRateToArticleCommand request, CancellationToken cancellationToken)
        {
            var articleToRate = await dbContext.Articles
                .SingleOrDefaultAsync(a => a.Id == request.id, cancellationToken);

            if (articleToRate is null)
                return;

            articleToRate.Rate = request.rate;
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
