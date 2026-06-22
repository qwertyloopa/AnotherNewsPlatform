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
    public class GetUserDataToChangeQueryHandler(AnpDbContext dbContext) : IRequestHandler<GetUserDataToChangeQuery, UserDto>
    {
        public async Task<UserDto> Handle(GetUserDataToChangeQuery request, CancellationToken cancellationToken)
        {
            var user = await dbContext.Users.AsNoTracking().SingleOrDefaultAsync(u => u.Id == request.Id);
            var mapper = new UserMapper();
            var userDto = mapper.ToDto(user);
            return userDto;
        }
    }
}
