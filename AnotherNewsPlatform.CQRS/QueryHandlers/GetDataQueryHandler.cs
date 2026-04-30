using AnotherNewsPlatform.Core.Mappers;
using AnotherNewsPlatform.CQS.Query;
using AnotherNewsPlatform.Database;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnotherNewsPlatform.CQS.QueryHandlers
{
    internal class GetDataQueryHandler(AnpDbContext dbContext) : IRequest<GetDataQuery>
    {
        public async Task<GetDataQuery> Handle(GetDataQuery request, CancellationToken cancellationToken)
        {
            var mapper = new ArticleEntityToDto();
            var articles = await dbContext.Articles.Select(n => mapper.ToDto(n)).ToList(cancellationToken);
            return articles;
        }
    }
}
