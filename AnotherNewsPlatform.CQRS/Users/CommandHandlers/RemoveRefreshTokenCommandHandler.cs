using AnotherNewsPlatform.CQS.Users.Commands;
using AnotherNewsPlatform.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AnotherNewsPlatform.CQS.Users.CommandHandlers;

public class RemoveRefreshTokenCommandHandler(AnpDbContext dbContext): IRequestHandler<RemoveRefreshTokenCommand>
{
    public async Task Handle(RemoveRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var token = await dbContext.RefreshTokens.FirstOrDefaultAsync(t => t.Id == request.refreshToken, cancellationToken);
        if (token != null)
        {
            dbContext.RefreshTokens.Remove(token);
        }
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}