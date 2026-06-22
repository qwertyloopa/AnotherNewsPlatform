
using MediatR;

namespace AnotherNewsPlatform.CQS.Articles.Commands;

public record UpdatePartialArticleCommand(Guid id, string? updatedArticleTitle, decimal? updatedArticleRate): IRequest;