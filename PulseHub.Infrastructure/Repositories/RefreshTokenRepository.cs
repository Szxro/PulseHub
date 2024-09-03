using Microsoft.EntityFrameworkCore;
using PulseHub.Domain.Contracts;
using PulseHub.Domain.Entities;
using PulseHub.Infrastructure.Common;
using PulseHub.Infrastructure.Persistence;

namespace PulseHub.Infrastructure.Repositories;

public class RefreshTokenRepository : GenericRepository<RefreshToken>, IRefreshTokenRepository
{
    public RefreshTokenRepository(AppDbContext dbContext) : base(dbContext) { }

    public async Task<List<RefreshToken>> GetExpiredUserRefreshTokenAsync(DateTime currentDateTime,CancellationToken cancellationToken = default)
    {
        return await _dbContext.RefreshToken.Where(x => !x.IsUsed && !x.IsRevoked && !x.IsExpired && x.ExpirationDate <= currentDateTime).ToListAsync(cancellationToken);
    }

    public async Task<RefreshToken?> GetUnusedUserRefreshTokenByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        return await _dbContext.RefreshToken.Include(x => x.User)
                                            .Where(x => x.User.Username == username && !x.IsUsed && !x.IsRevoked && !x.IsExpired)
                                            .FirstOrDefaultAsync(cancellationToken);
    }
}
