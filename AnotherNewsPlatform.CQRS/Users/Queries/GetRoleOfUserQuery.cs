using MediatR;

namespace AnotherNewsPlatform.CQS.Users.Queries;

public record GetRoleOfUserQuery(long RoleId): IRequest<string>;