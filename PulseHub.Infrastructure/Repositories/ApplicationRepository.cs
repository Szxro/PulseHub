using PulseHub.Domain.Contracts;
using DomainApplication = PulseHub.Domain.Entities.Application;
using PulseHub.Infrastructure.Common;
using PulseHub.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace PulseHub.Infrastructure.Repositories;

public class ApplicationRepository 
    : GenericRepository<DomainApplication>, IApplicationRepository
{
    public ApplicationRepository(AppDbContext dbContext) : base(dbContext) { }

    public async Task<DomainApplication?> GetApplicationAndAccessKeyByNameAsync(string applicationName,CancellationToken cancellationToken = default)
    {
        return await _dbContext.Application
                               .AsNoTracking()
                               .Include(x => x.AccessKeys)
                               .Where(x => x.Name == applicationName && x.AccessKeys.Any(x => x.IsActive))
                               .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<bool> IsApplicationNameNotUnique(string name)
    {
        return await _dbContext.Application.AsNoTracking().AnyAsync(x => x.Name == name);
    }
}
