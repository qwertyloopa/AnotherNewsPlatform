using AnotherNewsPlatform.Core.DTOs;
using MediatR;

namespace AnotherNewsPlatform.CQS.Articles.Query;

public record GetArticleCountCommand(): IRequest<IReadOnlyList<ArticleDto>>;