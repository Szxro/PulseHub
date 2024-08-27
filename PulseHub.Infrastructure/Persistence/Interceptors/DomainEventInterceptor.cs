using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PulseHub.SharedKernel;
using PulseHub.Infrastructure.Channels;

namespace PulseHub.Infrastructure.Persistence.Interceptors;

public class DomainEventInterceptor : SaveChangesInterceptor
{
    private readonly DomainEventChannel _eventChannel;

    public DomainEventInterceptor(DomainEventChannel eventChannel)
    {
        _eventChannel = eventChannel;
    }

    public override async ValueTask<int> SavedChangesAsync(
        SaveChangesCompletedEventData eventData,
        int result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is not null)
        {
            await DispatchEntitiesEvents(eventData.Context);
        }

        return await base.SavedChangesAsync(eventData, result, cancellationToken);
    }

    private async Task DispatchEntitiesEvents(DbContext dbContext,CancellationToken cancellationToken = default)
    {
        List<DomainEvent> entitiesEvents = dbContext.ChangeTracker.Entries<Entity>()
                                                    .Select(x => x.Entity)
                                                    .Where(entity => entity.DomainEvent.Any())
                                                    .SelectMany(entity => entity.DomainEvent)
                                                    .ToList();

        if (entitiesEvents.Any())
        {
            return;
        }


        foreach (DomainEvent entityEvent in entitiesEvents)
        {
            await _eventChannel.AddEventAsync(entityEvent,cancellationToken);
        } 
    }
}
