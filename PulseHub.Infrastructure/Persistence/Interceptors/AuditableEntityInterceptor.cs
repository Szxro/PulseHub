using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PulseHub.SharedKernel;

namespace PulseHub.Infrastructure.Persistence.Interceptors;

public class AuditableEntityInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is not null)
        {
            UpdateAuditableEntities(eventData.Context);
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdateAuditableEntities(DbContext dbContext)
    {
        List<EntityEntry<AuditableEntity>> entities = dbContext.ChangeTracker.Entries<AuditableEntity>()
                                                                             .Where(entity => entity.State == EntityState.Added || entity.State == EntityState.Modified)
                                                                             .ToList();

        foreach (EntityEntry<AuditableEntity> entity in entities)
        {
            if (entity.State == EntityState.Added)
            {
                SetCurrentProperty(entity, nameof(AuditableEntity.CreatedAtUtc), DateTime.UtcNow);
                SetCurrentProperty(entity, nameof(AuditableEntity.ModifiedAtUtc), DateTime.UtcNow);
            }

            if (entity.State == EntityState.Modified)
            {
                SetCurrentProperty(entity, nameof(AuditableEntity.ModifiedAtUtc), DateTime.UtcNow);
            }
        }
    }

    private void SetCurrentProperty(
        EntityEntry entity,
        string propertyName,
        DateTime dateTime) => entity.Property(propertyName).CurrentValue = dateTime; 
}
