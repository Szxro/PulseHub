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

    public async Task<bool> IsApplicationNameNotUnique(string name)
    {
        return await _dbContext.Application.AsNoTracking().AnyAsync(x => x.Name == name);
    }
}
