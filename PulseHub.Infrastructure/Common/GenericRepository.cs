using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using PulseHub.Infrastructure.Persistence;
using PulseHub.SharedKernel.Contracts;
using System.Linq.Expressions;

namespace PulseHub.Infrastructure.Common;

public abstract class GenericRepository<TEntity>
    where TEntity : class, IEntity
{
    protected readonly AppDbContext _dbContext;

    protected GenericRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(TEntity entity)
    {
        _dbContext.Set<TEntity>().Add(entity);
    }

    public void AddRange(IEnumerable<TEntity> entities)
    {
        _dbContext.Set<TEntity>().AddRange(entities);
    }

    public void Remove(TEntity entity)
    {
        _dbContext.Set<TEntity>().Remove(entity);
    }

    public void RemoveRange(IEnumerable<TEntity> entities)
    {
        _dbContext.Set<TEntity>().RemoveRange(entities);
    }

    public void Update(TEntity entity)
    {
        _dbContext.Set<TEntity>().Update(entity);
    }

    public void UpdateRange(IEnumerable<TEntity> entities)
    {
        _dbContext.Set<TEntity>().UpdateRange(entities);
    }

    public async Task<TEntity?> GetById(int id,CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<TEntity>().Where(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<int?> RemoveById(int id,CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<TEntity>().Where(x => x.Id == id).ExecuteDeleteAsync(cancellationToken);
    }

    public async Task<int?> BulkDelete(Expression<Func<TEntity,bool>> filter,CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<TEntity>().Where(filter).ExecuteDeleteAsync(cancellationToken);
    }

    public async Task<int?> BulkUpdate(
        Expression<Func<TEntity, bool>> filter,
        Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> properties,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<TEntity>().Where(filter).ExecuteUpdateAsync(properties,cancellationToken);
    }
}
