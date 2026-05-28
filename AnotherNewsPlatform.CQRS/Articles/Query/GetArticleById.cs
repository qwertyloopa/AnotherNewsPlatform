using AnotherNewsPlatform.Core.DTOs;
using MediatR;

namespace AnotherNewsPlatform.CQS.Articles.Query;

public record GetArticleById(Guid id) : IRequest<ArticleDto?>;
