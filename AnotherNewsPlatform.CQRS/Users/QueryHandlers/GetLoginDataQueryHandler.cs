using System;
using AnotherNewsPlatform.Core.DTOs;
using AnotherNewsPlatform.Core.Mappers;
using AnotherNewsPlatform.CQS.Users.Queries;
using AnotherNewsPlatform.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AnotherNewsPlatform.CQS.Users.QueryHandlers;

public class GetLoginDataQueryHandler(AnpDbContext dbContext): IRequestHandler<GetLoginDataQuery, UserDto>
{
    public async Task<UserDto> Handle(GetLoginDataQuery request, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users.AsNoTrackingWithIdentityResolution().Include(u => u.Role).SingleOrDefaultAsync(u => u.Email == request.Email, cancellationToken);
        if (user == null)
        {
            throw new Exception("User not found");
        }
        var mapper = new UserMapper();
        var userDto = mapper.ToDto(user);
        return userDto;
    }
}
