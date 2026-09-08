using AnotherNewsPlatform.Core.DTOs;
using AnotherNewsPlatform.Core.Exceptions;
using AnotherNewsPlatform.CQS.Sources.Commands;
using AnotherNewsPlatform.CQS.Sources.Queries;
using MediatR;

namespace AnotherNewsPlatform.Services.SourceService
{
    public class SourceService(IMediator mediator): ISourceService
    {
        public async Task<SourceDto> GetSourceAsync(long sourceId)
        {
            var response =  await mediator.Send(new GetSourceQuery(sourceId));
            if (response == null) return null;
            return response;
        }

        public async Task CreateSourceAsync(SourceDto source)
        {
            await mediator.Send(new CreateSourceAsyncCommand { Source = source });
        }

        public async Task DeleteSourceAsync(long sourceId)
        {
            await mediator.Send(new DeleteSourceCommand(sourceId));
        }

        public async Task UpdateSourceAsync(long sourceId, SourceDto source)
        {
            await mediator.Send(new UpdateSourceCommand(sourceId, source));
        }
    }
}

