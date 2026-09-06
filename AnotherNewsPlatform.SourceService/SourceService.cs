using AnotherNewsPlatform.Core.DTOs;
using AnotherNewsPlatform.CQS.Sources.Commands;
using AnotherNewsPlatform.CQS.Sources.Queries;
using MediatR;

namespace AnotherNewsPlatform.Services.SourceService
{
    public class SourceService(IMediator mediator): ISourceService
    {
        public async Task<SourceDto> GetSourceAsync(long sourceId)
        {
            return await mediator.Send(new GetSourceQuery(sourceId));
        }

        public async Task CreateSourceAsync(SourceDto source)
        {
            await mediator.Send(new CreateSourceAsyncCommand { Source = source });
        }
    }
}

