using AnotherNewsPlatform.Core.DTOs;
using AnotherNewsPlatform.Database;
using MediatR;

namespace AnotherNewsPlatform.CQS.Users.Queries;

public record GetLoginDataQuery(AnpDbContext DbContext) : IRequest<UserDto>
{
    public string Email { get; set; }
    public string Password { get; set; }
}
