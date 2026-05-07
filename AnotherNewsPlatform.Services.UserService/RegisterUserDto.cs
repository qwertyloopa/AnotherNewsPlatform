using System;

namespace AnotherNewsPlatform.Services.UserService;

public class RegisterUserDto
{
    public long Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public long RoleId { get; set; }
}
