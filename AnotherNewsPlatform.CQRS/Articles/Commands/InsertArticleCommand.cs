using AnotherNewsPlatform.Core.DTOs;
using MediatR;

namespace AnotherNewsPlatform.CQS.Articles.Commands;

public record InsertArticleCommand : IRequest
{
    public required ArticleDto Article { get; set; }
}
