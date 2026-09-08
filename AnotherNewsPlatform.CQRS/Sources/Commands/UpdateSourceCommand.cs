using AnotherNewsPlatform.Core.DTOs;
using MediatR;

namespace AnotherNewsPlatform.CQS.Sources.Commands;

public record UpdateSourceCommand(long id, SourceDto source): IRequest;