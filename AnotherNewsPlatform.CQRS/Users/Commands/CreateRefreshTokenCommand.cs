using MediatR;

namespace AnotherNewsPlatform.CQS.Users.Commands;

public record CreateRefreshTokenCommand() : IRequest
{
    public Guid UserId { get; init; }
    public Guid Token { get; init; }
    public string? DeviceName { get; init; }
}