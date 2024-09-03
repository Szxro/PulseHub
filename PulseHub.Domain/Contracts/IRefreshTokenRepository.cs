using PulseHub.Domain.Entities;

namespace PulseHub.Domain.Contracts;

public interface IRefreshTokenRepository : IRepositoryWriter<RefreshToken>
{
    Task<RefreshToken?> GetUnusedUserRefreshTokenByUsernameAsync(string username,CancellationToken cancellationToken = default);

    Task<List<RefreshToken>> GetExpiredUserRefreshTokenAsync(DateTime currentDateTime, CancellationToken cancellationToken = default);
}
