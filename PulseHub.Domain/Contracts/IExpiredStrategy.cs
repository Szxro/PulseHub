using PulseHub.SharedKernel;

namespace PulseHub.Domain.Contracts;

public interface IExpiredStrategy<T>
    where T : Entity
{
    Task<List<T>> GetExpiredEntitiesAsync(DateTime currentDateTime,CancellationToken cancellationToken = default);

    void MarkEntitiesAsExpired(List<T> entities);
}
