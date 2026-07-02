using AnotherNewsPlatform.Core.DTOs;
using MediatR;

namespace AnotherNewsPlatform.CQS.Users.Queries;

public record GetUserByRefreshTokenQuery(Guid RefreshToken): IRequest<UserDto?>;