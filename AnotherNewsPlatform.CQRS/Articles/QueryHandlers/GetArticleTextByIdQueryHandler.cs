using AnotherNewsPlatform.Core.DTOs;
using AnotherNewsPlatform.CQS.Articles.Query;
using AnotherNewsPlatform.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnotherNewsPlatform.CQS.Articles.QueryHandlers
{
    public class GetArticleTextByIdQueryHandler(AnpDbContext dbContext) : IRequestHandler<GetArticleTextByIdQuery, string?>
    {
        public async Task<string?> Handle(GetArticleTextByIdQuery request, CancellationToken cancellationToken)
        {
            var article = await dbContext.Articles.AsNoTracking().SingleOrDefaultAsync(a => a.Id == request.id);
            var text = article.Text;
            return text;
        }
    }
}
