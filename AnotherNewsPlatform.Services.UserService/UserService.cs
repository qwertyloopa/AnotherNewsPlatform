using AnotherNewsPlatform.Core.DTOs;
using AnotherNewsPlatform.Core.Mappers;
using AnotherNewsPlatform.CQS.Users.Queries;
using MediatR;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Net;
namespace AnotherNewsPlatform.Services.UserService;

public class UserService(AnpDbContext dbContext, IMediator mediator) : IUserService
{
    public async Task RegisterAsync(string username, string email, string password, CancellationToken token)
    {
        var role = await dbContext.Roles.SingleAsync(r => r.Name == "User");
        var PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
        var user = new User
        {
            Username = username,
            Email = email,
            PasswordHash = PasswordHash,
            RoleId = role.Id,
        };
        await dbContext.Users.AddAsync(user);
        await dbContext.SaveChangesAsync();

    }
    public async Task<ClaimsIdentity?> GetLoginDataAsync(string email, string password, CancellationToken token)
    {
        var user = await mediator.Send(new GetLoginDataQuery(dbContext){ Email = email , Password = password });
        if(user != null && BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role.Name),
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
        var userOnCheck = await dbContext.Users.AsNoTrackingWithIdentityResolution().SingleOrDefaultAsync(u => u.Email == email, token);
        var passwordCheck = BCrypt.Net.BCrypt.Verify(password, userOnCheck.PasswordHash);
        bool verification = userOnCheck != null && passwordCheck;
        return verification;
    }

}
