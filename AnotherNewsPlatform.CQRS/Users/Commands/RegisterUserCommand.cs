
using AnotherNewsPlatform.Core.DTOs;
using AnotherNewsPlatform.Database;
using MediatR;

namespace AnotherNewsPlatform.CQS.Users.Commands;
public record RegisterUserCommand : IRequest<>
{
    public AnpDbContext DbContext { get; set; }
    public UserDto User { get; set; }
}