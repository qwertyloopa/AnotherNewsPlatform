using MediatR;

namespace AnotherNewsPlatform.CQS.Users.Commands;

public record RevokeRefreshTokenCommand(Guid refreshToken): IRequest;