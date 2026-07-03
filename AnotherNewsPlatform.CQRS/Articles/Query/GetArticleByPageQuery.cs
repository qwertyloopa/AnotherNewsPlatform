using AnotherNewsPlatform.Core.DTOs;
using AnotherNewsPlatform.Database.Entities;
using MediatR;

namespace AnotherNewsPlatform.CQS.Articles.Query;

public record GetArticleByPageQuery(int pageNumber, int pageSize): IRequest<IReadOnlyCollection<ArticleDto>>;