using AnotherNewsPlatform.Core;
using AnotherNewsPlatform.Database;
using MediatR;
namespace AnotherNewsPlatform.Services.UserService;

public class UserService : IUserService
{
    private readonly AnpDbContext _dbContext;
    private readonly IMediator _mediator;

    public UserService(AnpDbContext dbContext, IMediator mediator)
    {
        _dbContext = dbContext;
        _mediator = mediator;
    }


}
