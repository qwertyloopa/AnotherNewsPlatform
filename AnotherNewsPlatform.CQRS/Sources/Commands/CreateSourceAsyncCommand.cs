using AnotherNewsPlatform.Core.DTOs;
using MediatR;

namespace AnotherNewsPlatform.CQS.Sources.Commands;

public record CreateSourceAsyncCommand : IRequest
{
    public SourceDto Source { get; set; }
}