using AnotherNewsPlatform.CQS.Users.Commands;
using AnotherNewsPlatform.Database;
using MediatR;

namespace AnotherNewsPlatform.CQS.Users.CommandHandlers
{
    public class DeleteUserCommandHandler(AnpDbContext dbContext) : IRequestHandler<DeleteUserCommand>
    {
        public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await dbContext.Users.FindAsync(request.Id);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            dbContext.Users.Remove(user);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}