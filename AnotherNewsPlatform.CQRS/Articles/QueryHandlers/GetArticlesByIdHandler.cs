using System;
using AnotherNewsPlatform.Core.DTOs;
using AnotherNewsPlatform.Core.Mappers;
using AnotherNewsPlatform.CQS.Articles.Query;
using AnotherNewsPlatform.Database;
using AnotherNewsPlatform.Database.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AnotherNewsPlatform.CQS.Articles.QueryHandlers;

public class GetArticleByIdHandler(AnpDbContext dbContext, ArticleMapper mapper) : IRequestHandler<GetArticleById, ArticleDto>
{
    public async Task<ArticleDto> Handle(GetArticleById request, CancellationToken cancellationToken)
    {
        var article = await dbContext.Articles.FirstOrDefaultAsync(article => article.Id == request.id, cancellationToken);
        return mapper.ToDto(article);
    }
}
