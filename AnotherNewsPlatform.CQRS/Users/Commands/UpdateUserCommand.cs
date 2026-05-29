using AnotherNewsPlatform.Core.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnotherNewsPlatform.CQS.Users.Commands
{
    public record UpdateUserCommand: IRequest
    {
        public UserDto User { get; set; }
    }
}
