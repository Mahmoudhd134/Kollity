namespace Domain.Identity.UserRefreshToken;

public interface IUserRefreshTokenRepository
{
    Task<UserRefreshToken> GetRefreshTokenForUser(Guid userId, string userAgent,
        CancellationToken cancellationToken = default);

    Task<UserRefreshToken> GetRefreshTokenForUserWithUser(Guid userId, string userAgent,
        CancellationToken cancellationToken = default);

    Task AddRefreshTokenWithUserAgentAsync(UserRefreshToken userRefreshToken,
        CancellationToken cancellationToken = default);

    Task Update(UserRefreshToken userRefreshToken, CancellationToken cancellationToken = default);
}