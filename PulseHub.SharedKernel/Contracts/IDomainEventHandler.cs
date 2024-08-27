using MediatR;

namespace PulseHub.SharedKernel.Contracts;

public interface IDomainEventHandler<TNotification> : INotificationHandler<TNotification>
    where TNotification : IDomainEvent
{ }
