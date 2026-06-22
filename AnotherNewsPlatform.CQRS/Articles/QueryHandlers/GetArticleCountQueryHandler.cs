using AnotherNewsPlatform.Core.DTOs;
using AnotherNewsPlatform.CQS.Articles.Query;
using AnotherNewsPlatform.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AnotherNewsPlatform.CQS.Articles.QueryHandlers;

public class GetArticleCountQueryHandler(AnpDbContext dbContext): IRequestHandler<GetArticleCountQuery, int>
{
    public async Task<int> Handle(GetArticleCountQuery request, CancellationToken cancellationToken)
    {
        return await dbContext.Articles.CountAsync(cancellationToken);
    }
}