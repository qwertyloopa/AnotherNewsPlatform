using AnotherNewsPlatform.Core.Exceptions;
using AnotherNewsPlatform.Core.Mappers;
using AnotherNewsPlatform.CQS.Sources.Commands;
using AnotherNewsPlatform.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AnotherNewsPlatform.CQS.Sources.CommandHandlers;

public class UpdateSourceCommandHandler(AnpDbContext dbContext): IRequestHandler<UpdateSourceCommand>
{
    public async Task Handle(UpdateSourceCommand request, CancellationToken cancellationToken)
    {
        var sourceToUpdate = await dbContext.Sources.FirstAsync(s => s.Id == request.id);
        //if (sourceToUpdate == null) return null; крч это надо дописать
        var mapper = new SourceMapper();
        
        sourceToUpdate = mapper.ToEntity(request.source);
        dbContext.Sources.Update(sourceToUpdate);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}