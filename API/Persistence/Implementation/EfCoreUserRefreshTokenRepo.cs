using Domain.Identity.UserRefreshToken;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Implementation;

public class EfCoreUserRefreshTokenRepo : IUserRefreshTokenRepository
{
    private readonly ApplicationDbContext _context;

    public EfCoreUserRefreshTokenRepo(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<UserRefreshToken> GetRefreshTokenForUser(Guid userId, string userAgent,
        CancellationToken cancellationToken = default)
    {
        return await _context.UserRefreshTokens.FirstOrDefaultAsync(
            urt => urt.UserId == userId && urt.UserAgent == userAgent,
            cancellationToken);
    }

    public async Task<UserRefreshToken> GetRefreshTokenForUserWithUser(Guid userId, string userAgent,
        CancellationToken cancellationToken = default)
    {
        return await _context.UserRefreshTokens
            .Include(x => x.User)
            .FirstOrDefaultAsync(
                urt => urt.UserId == userId && urt.UserAgent == userAgent,
                cancellationToken);
    }

    public Task AddRefreshTokenWithUserAgentAsync(UserRefreshToken userRefreshToken,
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_context.UserRefreshTokens.Add(userRefreshToken));
    }

    public Task Update(UserRefreshToken userRefreshToken, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_context.UserRefreshTokens.Update(userRefreshToken));
    }
}