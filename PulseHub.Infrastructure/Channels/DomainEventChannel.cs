using Microsoft.Extensions.Logging;
using PulseHub.SharedKernel;
using System.Threading.Channels;

namespace PulseHub.Infrastructure.Channels;

public class DomainEventChannel
{
    private readonly Channel<DomainEvent> _channel;
    private readonly ILogger<DomainEventChannel> _logger;
    
    private const int MaxEvents = 1_00;

    public DomainEventChannel(ILogger<DomainEventChannel> logger)
    {
        BoundedChannelOptions options = new BoundedChannelOptions(MaxEvents)
        {
            SingleWriter = false,
            SingleReader = true,
        };

        _channel = Channel.CreateBounded<DomainEvent>(options);

        _logger = logger;
    }

    public async Task<bool> AddEventAsync(DomainEvent @event,CancellationToken cancellationToken = default)
    {
        while (await _channel.Writer.WaitToWriteAsync(cancellationToken) && !cancellationToken.IsCancellationRequested)
        {
            if (_channel.Writer.TryWrite(@event))
            {
                _logger.LogInformation("The domain event {event} was enqueue into the channel",@event.GetType().Name);

                return true;
            }
        }

        return false;
    }

    public IAsyncEnumerable<DomainEvent> ReadAllAsync(CancellationToken cancellationToken = default)
        => _channel.Reader.ReadAllAsync(cancellationToken);
}
