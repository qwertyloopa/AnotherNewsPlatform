using System;

namespace AnotherNewsPlatform.Core.DTOs;

public class RegisterUserDto
{
    public long Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }

    public long RoleId { get; set; }
}
