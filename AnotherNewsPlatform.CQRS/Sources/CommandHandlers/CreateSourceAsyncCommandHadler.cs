using AnotherNewsPlatform.Core.DTOs;
using AnotherNewsPlatform.CQS.Sources.Commands;
using AnotherNewsPlatform.Database;
using Microsoft.EntityFrameworkCore;
using AnotherNewsPlatform.Core.Mappers;
using MediatR;

namespace AnotherNewsPlatform.CQS.Sources.CommandHandlers;

public class CreateSourceAsyncCommandHandler(AnpDbContext dbContext) : IRequestHandler<CreateSourceAsyncCommand>
{
    public async Task Handle(CreateSourceAsyncCommand request, CancellationToken cancellationToken)
    {
        var source = request.Source;
        var sourceEntity = new SourceMapper().ToEntity(source);
        dbContext.Sources.Add(sourceEntity);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}