using PulseHub.Domain.Entities;

namespace PulseHub.Domain.Contracts;

public interface IRefreshTokenRepository : IRepositoryWriter<RefreshToken>
{
    Task<RefreshToken?> GetUnusedUserRefreshTokenByUsernameAsync(string username,CancellationToken cancellationToken = default);
}
