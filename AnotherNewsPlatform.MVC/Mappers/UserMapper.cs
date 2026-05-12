using System;
using AnotherNewsPlatform.Core.DTOs;
using AnotherNewsPlatform.MVC.Models.User;
using Riok.Mapperly.Abstractions;

namespace AnotherNewsPlatform.MVC.Mappers;

[Mapper]
public partial class UserMapper
{
    public partial UserDto FromRegisterModelToDto(RegisterModel model);
    public partial ChangeUserModel ToModel(UserDto dto);
}
