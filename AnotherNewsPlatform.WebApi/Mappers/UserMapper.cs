using AnotherNewsPlatform.Core.DTOs;
using AnotherNewsPlatform.WebApi.Models;
using Riok.Mapperly.Abstractions;

namespace AnotherNewsPlatform.WebApi.Mappers;

[Mapper]
public partial class UserMapper
{
    public partial UserModel FromDtoToModel(UserDto dto);
    public partial UserDto FromModelToDto(UserModel model);
}