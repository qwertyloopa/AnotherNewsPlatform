using AnotherNewsPlatform.Core.DTOs;
using Riok.Mapperly.Abstractions;


namespace AnotherNewsPlatform.Core.Mappers.User
{
    [Mapper]
    public partial class UserEntityToDtoMapper
    {
        [MapperIgnoreSource(nameof(user.Role))]
        public partial UserDto ToDto(AnotherNewsPlatform.Database.Entities.User user);
    }
}
