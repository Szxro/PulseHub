using Microsoft.EntityFrameworkCore;
using PulseHub.Domain.Contracts;
using PulseHub.Domain.Entities;
using PulseHub.Infrastructure.Common;
using PulseHub.Infrastructure.Persistence;

namespace PulseHub.Infrastructure.Repositories;

public sealed class AvatarRepository : GenericRepository<Avatar>, IAvatarRepository
{
    public AvatarRepository(AppDbContext dbContext) : base(dbContext) { }

    public async Task<Avatar?> GetUserAvatarByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Avatar.Include(x => x.User)
                                      .Where(x => x.User!.Username == username)
                                      .FirstOrDefaultAsync(cancellationToken);
    }
}
