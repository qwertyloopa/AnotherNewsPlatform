using AnotherNewsPlatform.Core.DTOs;
using MediatR;

namespace AnotherNewsPlatform.CQS.Users.Commands;

public record RegisterUserCommand : IRequest
{
    public required UserDto User { get; set; }
}