using AnotherNewsPlatform.Core.DTOs;
using AnotherNewsPlatform.Database.Entities;
using Riok.Mapperly.Abstractions;


namespace AnotherNewsPlatform.Core.Mappers
{
    [Mapper]
    public partial class UserMapper
    {
        [MapProperty(nameof(User.Role.Name), nameof(UserDto.RoleName))]
        public partial UserDto ToDto(User user);
        
        [MapProperty(nameof(UserDto.RoleName), nameof(User.Role.Name))]
        public partial User ToEntity(UserDto userDto);

    }
}
