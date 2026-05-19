using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AnotherNewsPlatform.TokenService
{
    public class TokenService(IMediator dateTimeProvider, ILogger<TokenService> token): ITokenService
    {
        //public string GenerateAccessToken(ClaimsIdentity claimsIdentity)
        //{
        //    var jwtTokenHandler = new JwtSecurityTokenHandler();

        //}
    }
}
