using AnotherNewsPlatform.Core.DTOs;
using MediatR;

namespace AnotherNewsPlatform.CQS.Articles.Query;

public record GetArticleByRateAndSourceQuery(decimal? MinRate, int? SourceId): IRequest<IReadOnlyCollection<ArticleDto>>;