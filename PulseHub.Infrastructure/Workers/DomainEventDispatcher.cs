using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PulseHub.Domain.Contracts;
using PulseHub.Infrastructure.Channels;
using PulseHub.SharedKernel;

namespace PulseHub.Infrastructure.Workers;

public class DomainEventDispatcher : BackgroundService
{
    private readonly DomainEventChannel _eventChannel;
    private readonly IDomainEventDispatcherService _dispatcherService;
    private readonly ILogger<DomainEventDispatcher> _logger;

    public DomainEventDispatcher(
        DomainEventChannel eventChannel,
        IDomainEventDispatcherService dispatcherService,
        ILogger<DomainEventDispatcher> logger)
    {
        _eventChannel = eventChannel;
        _dispatcherService = dispatcherService;
        _logger = logger;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach (DomainEvent @event in _eventChannel.ReadAllAsync(stoppingToken))
        {
            try
            {
                await _dispatcherService.PublishDomainEvent(@event, stoppingToken);
            } catch
            {
                _logger.LogError("Failed to publish the domain event {eventName}, retrying....",@event.GetType().Name);

                await _dispatcherService.RetryPublishDomainEvent(@event, cancellationToken: stoppingToken);
            }
        }
    }
}
