using AnotherNewsPlatform.CQS.Users.Commands;
using AnotherNewsPlatform.Database;
using AnotherNewsPlatform.Database.Entities;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace AnotherNewsPlatform.CQS.Users.CommandHandlers;

public class CreateRefreshTokenCommandHandler(AnpDbContext dbContext, IConfiguration configuration) : IRequestHandler<CreateRefreshTokenCommand>
{
    public async Task Handle(CreateRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var refreshToken = new RefreshToken
        {
            UserId = request.UserId,
            Device = request.DeviceName,
            Expires = DateTime.UtcNow.AddDays(Convert.ToInt32(configuration["Jwt:RefreshTokenExpiryDays"])),
            IsRevoked = false,
            Id = request.Token,
        };
        await dbContext.RefreshTokens.AddAsync(refreshToken, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}