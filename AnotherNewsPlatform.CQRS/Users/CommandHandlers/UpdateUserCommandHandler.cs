using AnotherNewsPlatform.Core.Mappers;
using AnotherNewsPlatform.CQS.Users.Commands;
using AnotherNewsPlatform.Database;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnotherNewsPlatform.CQS.Users.CommandHandlers
{
    public class UpdateUserCommandHandler(AnpDbContext dbContext) : IRequestHandler<UpdateUserCommand>
    {
        public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var mapper = new UserMapper();
            var updatedUser = mapper.ToEntity(request.User);
            dbContext.Users.Update(updatedUser);
            await dbContext.SaveChangesAsync();
        }
    }
}
