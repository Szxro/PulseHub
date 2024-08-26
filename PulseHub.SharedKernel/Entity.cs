using PulseHub.SharedKernel.Contracts;
using System.ComponentModel.DataAnnotations.Schema;

namespace PulseHub.SharedKernel;

public class Entity : AuditableEntity, IEntity, ISoftDelete
{
    public int Id { get; set; }

    public bool IsDeleted { get; set;    }

    public DateTimeOffset DeletedAtUtc { get; set; } = new DateTime(1999, 01, 01, 01, 00, 00);

    private List<DomainEvent> domainEvents = new List<DomainEvent>();

    [NotMapped]
    public IReadOnlyCollection<DomainEvent> DomainEvent => domainEvents;

    public void AddEvent(DomainEvent @event)
    {
        domainEvents.Add(@event);
    }

    public void RemoveEvent(DomainEvent @event)
    {
        domainEvents.Remove(@event);
    }

    public void ClearEvents()
    {
        domainEvents.Clear();
    }
}
