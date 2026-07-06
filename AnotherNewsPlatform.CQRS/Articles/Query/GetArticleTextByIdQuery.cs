using AnotherNewsPlatform.Core.DTOs;
using MediatR;

namespace AnotherNewsPlatform.CQS.Articles.Query
{
    public record GetArticleTextByIdQuery(Guid id) : IRequest<string?>;
}