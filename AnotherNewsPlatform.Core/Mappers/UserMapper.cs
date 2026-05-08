using AnotherNewsPlatform.Core.DTOs;
using AnotherNewsPlatform.Database.Entities;
using Riok.Mapperly.Abstractions;


namespace AnotherNewsPlatform.Core.Mappers
{
    [Mapper]
    public partial class UserMapper
    {
        [MapperIgnoreSource(nameof(user.Role))]
        public partial UserDto ToDto(AnotherNewsPlatform.Database.Entities.User user);

        public partial AnotherNewsPlatform.Database.Entities.User ToEntity(UserDto userDto);
    }
}
