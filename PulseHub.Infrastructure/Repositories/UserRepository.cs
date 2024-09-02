using Microsoft.EntityFrameworkCore;
using PulseHub.Domain.Contracts;
using PulseHub.Domain.Entities;
using PulseHub.Domain.ValueObjects;
using PulseHub.Infrastructure.Common;
using PulseHub.Infrastructure.Persistence;

namespace PulseHub.Infrastructure.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(AppDbContext dbContext) : base(dbContext) { }

    public async Task<User?> GetUserByUsernameAndEmailAsync(string username, string email, CancellationToken cancellationToken = default)
    {
        return await _dbContext.User.Where(x => x.Username == username && x.Email.Value == email).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<User?> GetUserByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        return await _dbContext.User.Include(x => x.Credentials).Where(x => x.Username == username).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<bool> IsEmailUnique(Email email)
    {
        return await _dbContext.User.AsNoTracking().AnyAsync(user => user.Email.Value == email.Value);
    }

    public async Task<bool> IsUserNameUnique(string username)
    {
        return await _dbContext.User.AsNoTracking().AnyAsync(user => user.Username == username);
    }
}
