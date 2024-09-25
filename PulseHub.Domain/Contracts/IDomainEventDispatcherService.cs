using PulseHub.SharedKernel;

namespace PulseHub.Domain.Contracts;

public interface IDomainEventDispatcherService
{
    Task PublishDomainEvent(DomainEvent @event,CancellationToken cancellationToken = default);

    Task RetryPublishDomainEvent(DomainEvent @event,BackOffOptions? options = default,CancellationToken cancellationToken = default);
}
