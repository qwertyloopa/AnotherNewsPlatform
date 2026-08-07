using AnotherNewsPlatform.Core.DTOs;
using AnotherNewsPlatform.Database.Entities;
using Riok.Mapperly.Abstractions;


namespace AnotherNewsPlatform.Core.Mappers
{
    [Mapper]
    public partial class UserMapper
    {
        [MapperIgnoreSource(nameof(User.Role))]
        public partial UserDto ToDto(User user);

        [MapperIgnoreTarget(nameof(User.Role))]
        public partial User ToEntity(UserDto userDto);

    }
}
