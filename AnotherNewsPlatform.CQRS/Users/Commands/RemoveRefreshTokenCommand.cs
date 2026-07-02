using MediatR;

namespace AnotherNewsPlatform.CQS.Users.Commands;

public record RemoveRefreshTokenCommand(Guid refreshToken) : IRequest;
