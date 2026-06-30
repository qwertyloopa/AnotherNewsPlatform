using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace AnotherNewsPlatform.TokenService
{
    public interface ITokenService
    {
        string GenerateAccessToken(ClaimsIdentity claimsIdentity);
        Task<Guid> GenerateRefreshTokenAsync(Guid userId, string deviceName = null, CancellationToken cancellationToken = default );
    }
}
