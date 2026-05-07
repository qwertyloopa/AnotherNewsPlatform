using AnotherNewsPlatform.Core.DTOs;
using AnotherNewsPlatform.Database.Entities;
using Riok.Mapperly.Abstractions;


namespace AnotherNewsPlatform.Core.Mappers.User
{
    [Mapper]
    public partial class UserEntityToDtoMapper
    {
        [MapperIgnoreSource(nameof(user.Role))]
        public partial UserDto Map(AnotherNewsPlatform.Database.Entities.User user);
    }
}
