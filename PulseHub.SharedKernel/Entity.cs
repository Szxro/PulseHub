using PulseHub.SharedKernel.Contracts;

namespace PulseHub.SharedKernel;

public class Entity : AuditableEntity, IEntity, ISoftDelete
{
    public int Id { get; set; }

    public bool IsDeleted { get; set;    }

    public DateTimeOffset DeletedAtUtc { get; set; } = new DateTime(1999, 01, 01, 01, 00, 00);
}
