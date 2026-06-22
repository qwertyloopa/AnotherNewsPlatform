using AnotherNewsPlatform.Core.DTOs;
using AnotherNewsPlatform.Core.Mappers;
using AnotherNewsPlatform.CQS.Articles.Query;
using AnotherNewsPlatform.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AnotherNewsPlatform.CQS.Articles.QueryHandlers;

public class GetArticleByRateAndSourceQueryHandler(AnpDbContext dbContext): IRequestHandler<GetArticleByRateAndSourceQuery, IReadOnlyCollection<ArticleDto>>
{
    public async Task<IReadOnlyCollection<ArticleDto>> Handle(GetArticleByRateAndSourceQuery request, CancellationToken cancellationToken)
    {
        var articles = dbContext.Articles.AsNoTrackingWithIdentityResolution().AsQueryable();

        if (request.MinRate != null)
        {
            articles = articles.Where(a => a.Rate == request.MinRate);
        }

        if (request.SourceId != null)
        {
            articles = articles.Where(a => a.SourceId == request.SourceId);
        }
        
        var mapper = new ArticleMapper();
        return (await articles.Select(a => mapper.ToDto(a))
                .ToArrayAsync<ArticleDto>(cancellationToken))
                .AsReadOnly();
    }
}