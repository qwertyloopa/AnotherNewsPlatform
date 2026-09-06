using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnotherNewsPlatform.CQS.Sources.Commands
{
    public record DeleteSourceCommand(long id) : IRequest;
}
