using AnotherNewsPlatform.Core.DTOs;
using MediatR;

namespace AnotherNewsPlatform.CQS.Users.Queries;

public record GetLoginDataQuery : IRequest<UserDto>
{
    public required string Email { get; init; }
    public required string Password { get; init; }
}
