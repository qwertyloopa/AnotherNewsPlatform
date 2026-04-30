using MediatR;
using AnotherNewsPlatform.Core.DTOs;
using AnotherNewsPlatform.Database;


namespace AnotherNewsPlatform.CQS.Commands;

public record InsertArticleDataCommand(AnpDbContext context) : IRequest
{
    public required IEnumerable<ArticleDto> Articles { get; init; }
}
