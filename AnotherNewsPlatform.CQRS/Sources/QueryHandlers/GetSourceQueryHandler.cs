using AnotherNewsPlatform.Core.DTOs;
using AnotherNewsPlatform.Core.Mappers;
using AnotherNewsPlatform.CQS.Sources.Queries;
using AnotherNewsPlatform.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnotherNewsPlatform.CQS.Sources.QueryHandlers
{
    public class GetSourceQueryHandler(AnpDbContext dbContext): IRequestHandler<GetSourceQuery, SourceDto>
    {
        public async Task<SourceDto> Handle(GetSourceQuery request, CancellationToken cancellationToken)
        {
            var result = await dbContext.Sources.FirstOrDefaultAsync(s => s.Id == request.SourceId, cancellationToken);
            var mapper = new SourceMapper();
            return mapper.ToDto(result);
        }
    }
}
