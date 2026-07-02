using MediatR;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AnotherNewsPlatform.Core.DTOs;
using AnotherNewsPlatform.Core.Exceptions;
using AnotherNewsPlatform.CQS.Users.Commands;
using AnotherNewsPlatform.CQS.Users.Queries;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AnotherNewsPlatform.TokenService
{
    public class TokenService(IConfiguration configuration, IMediator mediator, ILogger<TokenService> logger): ITokenService
    {
        public string GenerateAccessToken(UserDto userDto)
        {
            try
            {
                var jwtTokenHandler = new JwtSecurityTokenHandler();
                var secretKey = Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]);

                //var role = mediator.Send(new GetRoleOfUserQuery(userDto.RoleId));
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("ID", userDto.Id.ToString()),
                        new Claim(ClaimTypes.Name, userDto.Username),
                        new Claim(ClaimTypes.Email, userDto.Email),
                        new Claim(ClaimTypes.Role,  userDto.RoleName)
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(configuration["Jwt:ExpireMinutes"])),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature),
                    Audience = configuration["Jwt:Audience"],
                    Issuer = configuration["Jwt:Issuer"],
                    IssuedAt = DateTime.UtcNow,
                    //NotBefore = DateTime.UtcNow,
                };
                
                var token = jwtTokenHandler.CreateToken(tokenDescriptor);
                return jwtTokenHandler.WriteToken(token);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error occured while generating token");
                throw new InternalServerErrorException("Error occured while generating token", e);
            }
        }

        public async Task<Guid> GenerateRefreshTokenAsync(long userId, string deviceName = null, CancellationToken cancellationToken = default)
        {
            var refreshToken = Guid.NewGuid();
            
            await mediator.Send(new CreateRefreshTokenCommand()
            {
                DeviceName = deviceName,
                Token = refreshToken,
                UserId =  userId
            });
            
            return refreshToken;
        }
        

        public async Task RemoveRefreshTokenAsync(Guid refreshToken, CancellationToken cancellationToken = default)
        {
            await mediator.Send(new RemoveRefreshTokenCommand(refreshToken));
        }
    }
}
