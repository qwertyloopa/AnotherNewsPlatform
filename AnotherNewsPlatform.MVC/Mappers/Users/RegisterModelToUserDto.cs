using System;
using AnotherNewsPlatform.Core.DTOs;
using AnotherNewsPlatform.MVC.Models.User;
using Riok.Mapperly.Abstractions;

namespace AnotherNewsPlatform.MVC.Mappers.Users;

[Mapper]
public partial class RegisterModelToUserDto
{
    public partial UserDto ToUserDto(RegisterModel model);
}
