using AnotherNewsPlatform.Core.DTOs;
using AnotherNewsPlatform.Database;
using MediatR;

namespace AnotherNewsPlatform.CQS.Articles.Commands;

public record InsertArticleCommand(AnpDbContext DbContext) : IRequest
{
    public required ArticleDto Article { get; set; }
}
