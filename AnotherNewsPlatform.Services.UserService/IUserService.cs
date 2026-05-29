using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using AnotherNewsPlatform.Core.DTOs;

namespace AnotherNewsPlatform.Services.UserService
{
    public interface IUserService
    {
        Task<ClaimsIdentity?> GetLoginDataAsync(string email, string password, CancellationToken token);
        Task RegisterAsync(string username, string email, string password, CancellationToken token);
        Task<bool> VerifyEmailAsync(string email, CancellationToken token);
        Task<bool> VerifyUserAsync(string email, string password, CancellationToken token);
        Task UpdateUserAsync(UserDto user);
        Task<UserDto> GetUserDtoAsync(long id);
    }
}
