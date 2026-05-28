using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnotherNewsPlatform.CQS.Users.Queries
{
    public record VerifyUserQuery : IRequest<bool>
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
