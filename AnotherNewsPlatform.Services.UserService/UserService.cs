using AnotherNewsPlatform.Core.DTOs;
using AnotherNewsPlatform.Core.Mappers.User;
using AnotherNewsPlatform.Database;
using MediatR;
using BCrypt.Net;
namespace AnotherNewsPlatform.Services.UserService;

public class UserService(AnpDbContext dbContext, IMediator mediator, UserEntityToDtoMapper inputMapper, UserDtoToEntityMapper outputMapper) : IUserService
{
    public object Register(UserDto userDto)
    {
        
    }
    

}
