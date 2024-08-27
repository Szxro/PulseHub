using PulseHub.SharedKernel.Contracts;

namespace PulseHub.Domain.Contracts;

public interface IRepositoryReader<TEntity>
    where TEntity : IEntity
{
    Task<TEntity?> GetById(int id);
}
