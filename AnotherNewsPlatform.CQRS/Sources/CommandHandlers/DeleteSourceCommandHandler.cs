using AnotherNewsPlatform.CQS.Sources.Commands;
using AnotherNewsPlatform.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnotherNewsPlatform.CQS.Sources.CommandHandlers
{
    public class DeleteSourceCommandHandler(AnpDbContext dbContext) : IRequestHandler<DeleteSourceCommand>
    {
        public async Task Handle(DeleteSourceCommand request, CancellationToken cancellationToken)
        {
            var source = await dbContext.Sources.FirstAsync(s => s.Id == request.id);
            dbContext.Sources.Remove(source);
            await dbContext.SaveChangesAsync();
        }
    }
}
