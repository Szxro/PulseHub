using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using PulseHub.Domain.Contracts;
using PulseHub.Infrastructure.Persistence;
using System.Data;

namespace PulseHub.Infrastructure.Common;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _dbContext;
    private readonly ILogger<UnitOfWork> _logger;

    public UnitOfWork(
        AppDbContext dbContext,
        ILogger<UnitOfWork> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<IDbTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            IDbContextTransaction transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

            return transaction.GetDbTransaction();

        } catch (Exception ex)
        {
            _logger.LogError("An exception happen trying to get a DbTransaction with the error message : {message}",ex.Message);

            throw;
        }
    }

    public void ChangeTrackerToUnchanged(object entity)
    {
        _dbContext.Entry(entity).State = EntityState.Unchanged;
    }

    public async Task<int?> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);

        } catch (Exception ex)
        {
            _logger.LogError(
                "An exception happen trying to save changes into the database {provider} with the error message : {message}",
                _dbContext.Provider,
                ex.Message);

            throw;
        }
    }
}
