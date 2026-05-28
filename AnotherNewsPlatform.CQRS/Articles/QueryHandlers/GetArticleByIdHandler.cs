using AnotherNewsPlatform.Core.DTOs;
using AnotherNewsPlatform.Core.Mappers;
using AnotherNewsPlatform.CQS.Articles.Query;
using AnotherNewsPlatform.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AnotherNewsPlatform.CQS.Articles.QueryHandlers;

public class GetArticleByIdHandler(AnpDbContext dbContext) : IRequestHandler<GetArticleById, ArticleDto?>
{
    public async Task<ArticleDto?> Handle(GetArticleById request, CancellationToken cancellationToken)
    {
        var mapper = new ArticleMapper();
        var article = await dbContext.Articles.FirstOrDefaultAsync(a => a.Id == request.id, cancellationToken);
        return article is null ? null : mapper.ToDto(article);
    }
}
