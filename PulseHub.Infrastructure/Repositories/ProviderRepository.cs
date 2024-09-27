using Microsoft.EntityFrameworkCore;
using PulseHub.Domain.Contracts;
using PulseHub.Domain.Entities;
using PulseHub.Infrastructure.Common;
using PulseHub.Infrastructure.Persistence;

namespace PulseHub.Infrastructure.Repositories;

public class ProviderRepository : GenericRepository<Provider>, IProviderRepository
{
    public ProviderRepository(AppDbContext dbContext) : base(dbContext) { }

    public async Task<Provider?> GetProviderByNameAsync(string providerName)
    {
        return await _dbContext.Provider.Where(provider => provider.Name == providerName).FirstOrDefaultAsync();
    }
}
