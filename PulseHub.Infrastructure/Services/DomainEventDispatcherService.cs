using MediatR;
using Microsoft.Extensions.Logging;
using PulseHub.Domain.Contracts;
using PulseHub.SharedKernel;

namespace PulseHub.Infrastructure.Services;

public class DomainEventDispatcherService : IDomainEventDispatcherService
{
    private readonly IPublisher _publisher;
    private readonly ILogger<DomainEventDispatcherService> _logger;

    private readonly Random _random = new Random();

    public DomainEventDispatcherService(
        IPublisher publisher,
        ILogger<DomainEventDispatcherService> logger)
    {
        _publisher = publisher;
        _logger = logger;
    }

    public async Task PublishDomainEvent(DomainEvent @event, CancellationToken cancellationToken = default)
    {
        try
        {
            await _publisher.Publish(@event, cancellationToken);

            _logger.LogInformation("Succesfully publish the event {eventName}",@event.GetType().Name);

        } catch (Exception ex)
        {
            _logger.LogError(
                "An unexpected error happen while trying to publish the domain event {eventName} with the error message : {message}",
                @event.GetType().Name,
                ex.Message);

            throw;
        }
    }

    public async Task RetryPublishDomainEvent(DomainEvent @event, BackOffOptions? options = default, CancellationToken cancellationToken = default)
    {
       (int maxRetries,int initialDelay,int maxDelay,int timeMultiple) = options ?? new BackOffOptions();
        
        int attempts = 0;

        while (attempts < maxRetries)
        {
            try
            {
                await _publisher.Publish(@event, cancellationToken);

                _logger.LogInformation(
                    "Successfully publish the event {eventName} after retry count {attempts}/{maxRetries}",
                    @event.GetType().Name,
                    attempts,
                    maxRetries);

                return;
            }
            catch
            {
                double waitTime = Math.Min(initialDelay * Math.Pow(timeMultiple, attempts), maxDelay);

                int delay = (int)Math.Floor(_random.NextDouble() * waitTime);

                await Task.Delay(delay, cancellationToken);

                attempts++;

                _logger.LogError(
                    "The domain event {eventName} failed to be published after retry count {attempts}/{maxRetries} and {delay}ms",
                    @event.GetType().Name,
                    attempts,
                    maxRetries,
                    delay);
            }
        }

        _logger.LogError("Max attempts reached, while trying to publishing the domain event {domainEvent}",@event.GetType().Name);
    }
}
