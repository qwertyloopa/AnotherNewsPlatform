using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using AnotherNewsPlatform.Core.DTOs;

namespace AnotherNewsPlatform.TokenService
{
    public interface ITokenService
    {
        string GenerateAccessToken(UserDto userDto);
        Task<Guid> GenerateRefreshTokenAsync(long userId, string deviceName = null, CancellationToken cancellationToken = default );
        Task RemoveRefreshTokenAsync(Guid refreshToken, CancellationToken cancellationToken);
    }
}
