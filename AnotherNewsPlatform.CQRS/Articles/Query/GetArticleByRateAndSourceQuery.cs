using AnotherNewsPlatform.Core.DTOs;
using MediatR;

namespace AnotherNewsPlatform.CQS.Articles.Query;

public record GetArticleByRateAndSource(decimal minRate, int sourceId): IRequest<IReadOnlyCollection<ArticleDto>>;