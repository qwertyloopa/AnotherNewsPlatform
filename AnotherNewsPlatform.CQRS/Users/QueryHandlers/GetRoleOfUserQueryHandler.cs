using AnotherNewsPlatform.CQS.Users.Queries;
using AnotherNewsPlatform.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AnotherNewsPlatform.CQS.Users.QueryHandlers;

public class GetRoleOfUserQueryHandler(AnpDbContext dbContext): IRequestHandler<GetRoleOfUserQuery, string>
{
    public async Task<string> Handle(GetRoleOfUserQuery request, CancellationToken cancellationToken)
    {
        var role = dbContext.Roles.SingleAsync(r => r.Id == request.RoleId).Result.Name;
        return role;
    }
}