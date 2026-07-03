using AnotherNewsPlatform.CQS.Users.Commands;
using AnotherNewsPlatform.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AnotherNewsPlatform.CQS.Users.CommandHandlers;

public class RevokeRefreshTokenCommandHandler(AnpDbContext dbContext): IRequestHandler<RevokeRefreshTokenCommand>
{
    public async Task Handle(RevokeRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var token = await dbContext.RefreshTokens.SingleOrDefaultAsync(t => t.Id == request.refreshToken,  cancellationToken);
        token.IsRevoked = true;
        dbContext.RefreshTokens.Update(token);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}