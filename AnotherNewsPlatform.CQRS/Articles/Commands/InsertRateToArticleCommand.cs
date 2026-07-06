using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnotherNewsPlatform.CQS.Articles.Commands
{
    public record InsertRateToArticleCommand(Guid id, decimal rate) : IRequest;
}
