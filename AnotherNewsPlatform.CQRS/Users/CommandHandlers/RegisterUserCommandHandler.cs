using AnotherNewsPlatform.Core.Mappers;
using AnotherNewsPlatform.CQS.Users.Commands;
using AnotherNewsPlatform.Database;
using MediatR;

namespace AnotherNewsPlatform.CQS.Users.CommandHandlers;

public class RegisterUserCommandHandler(AnpDbContext dbContext) : IRequestHandler<RegisterUserCommand>
{
    public async Task Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var mapper = new UserMapper();
        var userEntity = mapper.ToEntity(request.User);
        dbContext.Users.Add(userEntity);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
