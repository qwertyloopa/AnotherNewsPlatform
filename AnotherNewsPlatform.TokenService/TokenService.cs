using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AnotherNewsPlatform.Core.Exceptions;
using AnotherNewsPlatform.CQS.Users.Commands;
using Microsoft.Extensions.Configuration;

namespace AnotherNewsPlatform.TokenService
{
    public class TokenService(IConfiguration configuration, IMediator mediator, ILogger<TokenService> logger): ITokenService
    {
        public string GenerateAccessToken(ClaimsIdentity claimsIdentity)
        {
            try
            {
                var jwtTokenHandler = new JwtSecurityTokenHandler();
                var secretKey = Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claimsIdentity,
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

        public async Task<Guid> GenerateRefreshTokenAsync(Guid userId, string deviceName = null, CancellationToken cancellationToken = default)
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
    }
}
