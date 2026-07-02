using AnotherNewsPlatform.Core.DTOs;
using AnotherNewsPlatform.Core.Mappers;
using AnotherNewsPlatform.CQS.Users.Queries;
using AnotherNewsPlatform.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AnotherNewsPlatform.CQS.Users.QueryHandlers;

public class GetUserByRefreshTokenQueryHandler(AnpDbContext dbContext): IRequestHandler<GetUserByRefreshTokenQuery, UserDto?>
{
    public async Task<UserDto?> Handle(GetUserByRefreshTokenQuery request, CancellationToken cancellationToken)
    {
        var token = await dbContext.RefreshTokens.AsNoTrackingWithIdentityResolution().Include(rt => rt.User)
            .SingleOrDefaultAsync(token => token.Id == request.RefreshToken, cancellationToken);
        if (token == null) return null;
        var user = token.User;
        if (user == null) return null;
        return new UserMapper().ToDto(user);
    }
}