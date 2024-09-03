using PulseHub.Domain.Contracts;
using PulseHub.Domain.Entities;

namespace PulseHub.Infrastructure.Strategies;

public class RefreshTokenExpirationStrategy : IExpiredStrategy<RefreshToken>
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public RefreshTokenExpirationStrategy(IRefreshTokenRepository refreshTokenRepository)
    {
        _refreshTokenRepository = refreshTokenRepository;
    }
    public async Task<List<RefreshToken>> GetExpiredEntitiesAsync(DateTime currentDateTime, CancellationToken cancellationToken = default)
    {
        return await _refreshTokenRepository.GetExpiredUserRefreshTokenAsync(currentDateTime, cancellationToken);
    }

    public void MarkEntitiesAsExpired(List<RefreshToken> entities)
    {
        entities.ForEach(e => e.IsExpired = true);

        _refreshTokenRepository.UpdateRange(entities);
    }
}