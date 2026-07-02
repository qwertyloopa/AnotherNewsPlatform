using AnotherNewsPlatform.Core.DTOs;
using AnotherNewsPlatform.Database.Entities;
using Riok.Mapperly.Abstractions;


namespace AnotherNewsPlatform.Core.Mappers
{
    [Mapper]
    public partial class UserMapper
    {
        [MapProperty(nameof(AnotherNewsPlatform.Database.Entities.User.Role.Name), nameof(UserDto.RoleName))]
        public partial UserDto ToDto(AnotherNewsPlatform.Database.Entities.User user);
        
        [MapProperty(nameof(UserDto.RoleName), nameof(AnotherNewsPlatform.Database.Entities.User.Role.Name))]
        public partial AnotherNewsPlatform.Database.Entities.User ToEntity(UserDto userDto);

    }
}
