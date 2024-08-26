using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using PulseHub.Infrastructure.Persistence;
using PulseHub.SharedKernel.Contracts;
using System.Linq.Expressions;

namespace PulseHub.Infrastructure.Common;

public abstract class GenericRepository<TEntity>
    where TEntity : class, IEntity
{
    private readonly AppDbContext _dbContext;

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

    public async Task<TEntity?> GetById(int id)
    {
        return await _dbContext.Set<TEntity>().Where(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<int?> BulkDelete(Expression<Func<TEntity,bool>> filter)
    {
        return await _dbContext.Set<TEntity>().Where(filter).ExecuteDeleteAsync();
    }

    public async Task<int?> BulkUpdate(
        Expression<Func<TEntity, bool>> filter,
        Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> properties)
    {
        return await _dbContext.Set<TEntity>().Where(filter).ExecuteUpdateAsync(properties);
    }
}
