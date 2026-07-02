using AnotherNewsPlatform.Core.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnotherNewsPlatform.CQS.Users.Queries
{
    public record GetUserDataQuery: IRequest<UserDto>
    {
        public long Id { get; set; }
    }
}
