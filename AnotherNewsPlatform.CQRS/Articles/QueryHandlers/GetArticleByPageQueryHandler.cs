using AnotherNewsPlatform.Core.DTOs;
using AnotherNewsPlatform.Core.Mappers;
using AnotherNewsPlatform.CQS.Articles.Query;
using AnotherNewsPlatform.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AnotherNewsPlatform.CQS.Articles.QueryHandlers;

public class GetArticleByPageQueryHandler(AnpDbContext dbContext): IRequestHandler<GetArticleByPageQuery, IReadOnlyCollection<ArticleDto>>
{
    public async Task<IReadOnlyCollection<ArticleDto>> Handle(GetArticleByPageQuery request, CancellationToken cancellationToken)
    {

        var mapper = new ArticleMapper();
        var dtos = dbContext.Articles
            .AsNoTracking()
            .OrderByDescending(a => a.PublishDate)
            .Skip((request.pageNumber - 1) * request.pageSize)
            .Take(request.pageSize)
            .AsEnumerable()                // дальше — в памяти
            .Select(a => mapper.ToDto(a))
            .OrderByDescending(d => d.PublishDate)
            .ToArray();
        return (dtos).AsReadOnly();
    }
}