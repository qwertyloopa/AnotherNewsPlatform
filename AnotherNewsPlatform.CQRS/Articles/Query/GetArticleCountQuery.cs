using AnotherNewsPlatform.Core.DTOs;
using MediatR;

namespace AnotherNewsPlatform.CQS.Articles.Query;

public record GetArticleCountQuery(): IRequest<int>;