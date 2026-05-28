using AnotherNewsPlatform.Core.DTOs;
using AnotherNewsPlatform.Core.Mappers;
using AnotherNewsPlatform.CQS.Users.Queries;
using AnotherNewsPlatform.CQS.Users.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using AnotherNewsPlatform.Database;
using AnotherNewsPlatform.Database.Entities;
// using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
// using System.Net;
namespace AnotherNewsPlatform.Services.UserService;

using BCrypt.Net;

public class UserService(AnpDbContext dbContext, IMediator mediator, UserMapper mapper) : IUserService
{
    public async Task RegisterAsync(string username, string email, string password, CancellationToken token)
    {
        var role = await dbContext.Roles.SingleAsync(r => r.Name == "User");
        var passwordHash = BCrypt.HashPassword(password);
        var user = new UserDto
        {
            Username = username,
            Email = email,
            PasswordHash = passwordHash,
            RoleId = role.Id,
        };
        await mediator.Send(new RegisterUserCommand { User = user }, token);

    }
    public async Task<ClaimsIdentity?> GetLoginDataAsync(string email, string password, CancellationToken token)
    {
        var user = await mediator.Send(new GetLoginDataQuery { Email = email, Password = password });
        if(user != null && BCrypt.Verify(password, user.PasswordHash))
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, dbContext.Roles.Single(r => r.Id == user.RoleId).Name),
                new Claim("ID", user.Id.ToString()),
            };
            return new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        }
        throw new Exception("Invalid email or password");
    }
    
    public async Task<bool> VerifyEmailAsync(string email, CancellationToken token)
    {
        return await dbContext.Users.AsNoTrackingWithIdentityResolution().AnyAsync(u => u.Email == email, token);
    }

    public async Task<bool> VerifyUserAsync(string email, string password, CancellationToken token)
    {
        var verification = await mediator.Send(new VerifyUserQuery() { Email = email, Password = password});
        return verification;
    }

}
