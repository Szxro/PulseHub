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

    public async Task<bool> IsEmailUnique(Email email)
    {
        return await _dbContext.User.AsNoTracking().AnyAsync(user => user.Email.Value == email.Value);
    }

    public async Task<bool> IsUserNameUnique(string username)
    {
        return await _dbContext.User.AsNoTracking().AnyAsync(user => user.Username == username);
    }
}
