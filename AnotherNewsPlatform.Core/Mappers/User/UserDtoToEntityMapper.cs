using Riok.Mapperly.Abstractions;
using AnotherNewsPlatform.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnotherNewsPlatform.Core.Mappers.User
{
    [Mapper]
    public partial class UserDtoToEntityMapper
    {
        [MapperIgnoreTarget(nameof(AnotherNewsPlatform.Database.Entities.User.Role))]
        public partial AnotherNewsPlatform.Database.Entities.User ToEntity(UserDto userDto);
    }
}
