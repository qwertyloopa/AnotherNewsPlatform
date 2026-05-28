using AnotherNewsPlatform.CQS.Users.Queries;
using MediatR;
using AnotherNewsPlatform.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnotherNewsPlatform.CQS.Users.QueryHandlers
{
    using BCrypt.Net;

    public class VerifyUserQueryHandler(AnpDbContext dbContext) : IRequestHandler<VerifyUserQuery, bool>
    {
        public async Task<bool> Handle(VerifyUserQuery request, CancellationToken cancellationToken)
        {
            var userOnCheck = await dbContext.Users.AsNoTrackingWithIdentityResolution().SingleOrDefaultAsync(u => u.Email == request.Email, cancellationToken);
            var passwordCheck = BCrypt.Verify(request.Password, userOnCheck.PasswordHash);
            var verification = userOnCheck != null && passwordCheck;
            return verification;
        }
    }
}
