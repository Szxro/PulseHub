using Microsoft.Extensions.Logging;
using PulseHub.Domain.Contracts;
using PulseHub.SharedKernel;

namespace PulseHub.Infrastructure.Strategies.Context;

public class ExpirationStrategyContext<T>
    where T : Entity
{
    private readonly IExpiredStrategy<T> _strategy;
    private readonly ILogger<ExpirationStrategyContext<T>> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public ExpirationStrategyContext(
        IExpiredStrategy<T> strategy,
        ILogger<ExpirationStrategyContext<T>> logger,
        IUnitOfWork unitOfWork)
    {
        _strategy = strategy;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task ExecuteAsync(
        DateTime dateTime,
        CancellationToken cancellationToken = default)
    {
        try 
        {
            List<T> expiredEntities = await _strategy.GetExpiredEntitiesAsync(dateTime, cancellationToken);

            if (expiredEntities.Count <= 0)
            {
                _logger.LogInformation("Not expired '{entityName}' entities found", typeof(T).Name);

                return;
            }

            _logger.LogWarning(
                "Found {length} expired '{entityName}' entities, proceding to mark them as expired",
                expiredEntities.Count,
                typeof(T).Name);

            _strategy.MarkEntitiesAsExpired(expiredEntities);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

        } catch (Exception ex)
        {
            _logger.LogError(
                "An unexpected error happen while processing expired {name} entitites  with the error message : '{message}'",
                typeof(T).Name,
                ex.Message);
        }
    }
}
