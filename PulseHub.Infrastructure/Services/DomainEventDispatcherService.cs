using MediatR;
using Microsoft.Extensions.Logging;
using PulseHub.Domain.Contracts;
using PulseHub.SharedKernel;

namespace PulseHub.Infrastructure.Services;

public class DomainEventDispatcherService : IDomainEventDispatcherService
{
    private readonly IPublisher _publisher;
    private readonly ILogger<DomainEventDispatcherService> _logger;

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

    public async Task RetryPublishDomainEvent(DomainEvent @event, int maxRetries = 3, CancellationToken cancellationToken = default)
    {
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
                attempts++;

                _logger.LogError(
                    "The domain event {eventName} failed to be published after retry count {attempts}/{maxRetries}",
                    @event.GetType().Name,
                    attempts,
                    maxRetries);
            }
        }

        _logger.LogError("Max attempts reached, while trying to publishing the domain event {domainEvent}",@event.GetType().Name);
    }
}
