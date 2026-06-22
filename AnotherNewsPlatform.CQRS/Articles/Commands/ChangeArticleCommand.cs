using AnotherNewsPlatform.Core.DTOs;
using MediatR;

namespace AnotherNewsPlatform.CQS.Articles.Commands;

public record ChangeArticleCommand(ArticleDto article): IRequest;