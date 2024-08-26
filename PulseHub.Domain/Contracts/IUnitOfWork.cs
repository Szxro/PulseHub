using System.Data;

namespace PulseHub.Domain.Contracts;

public interface IUnitOfWork
{
    Task<int?> SaveChangesAsync(CancellationToken cancellationToken = default);

    void ChangeTrackerToUnchanged(object entity);

    Task<IDbTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
}
