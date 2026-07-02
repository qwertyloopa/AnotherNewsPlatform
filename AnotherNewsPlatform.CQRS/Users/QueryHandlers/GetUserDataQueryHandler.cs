using AnotherNewsPlatform.Core.DTOs;
using AnotherNewsPlatform.Core.Mappers;
using AnotherNewsPlatform.CQS.Users.Queries;
using AnotherNewsPlatform.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnotherNewsPlatform.CQS.Users.QueryHandlers
{
    public class GetUserDataQueryHandler(AnpDbContext dbContext) : IRequestHandler<GetUserDataQuery, UserDto>
    {
        public async Task<UserDto> Handle(GetUserDataQuery request, CancellationToken cancellationToken)
        {
            var user = await dbContext.Users.AsNoTrackingWithIdentityResolution().Include(u => u.Role)
                .SingleOrDefaultAsync(u => u.Id == request.Id);
            var mapper = new UserMapper();
            var userDto = mapper.ToDto(user);
            return userDto;
        }
    }
}
