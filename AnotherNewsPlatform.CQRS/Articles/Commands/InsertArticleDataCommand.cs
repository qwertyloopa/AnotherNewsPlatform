using MediatR;
using AnotherNewsPlatform.Core.DTOs;

namespace AnotherNewsPlatform.CQS.Articles.Commands;

public record InsertArticleDataCommand : IRequest
{
    public required IEnumerable<ArticleDto> Articles { get; init; }
}
