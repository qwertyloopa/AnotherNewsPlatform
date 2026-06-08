using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace AnotherNewsPlatform.CQS.Users.Commands
{
    public record DeleteUserCommand : IRequest
    {
        public long Id { get; set; }
    }
}