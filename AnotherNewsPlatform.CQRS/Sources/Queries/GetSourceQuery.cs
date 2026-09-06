using AnotherNewsPlatform.Core.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnotherNewsPlatform.CQS.Sources.Queries
{
    public record GetSourceQuery(long SourceId) : IRequest<SourceDto>;

}
