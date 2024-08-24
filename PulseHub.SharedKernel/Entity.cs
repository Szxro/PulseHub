using PulseHub.SharedKernel.Contracts;

namespace PulseHub.SharedKernel;

public class Entity : AuditableEntity, ISoftDelete
{
    public int Id { get; set; }

    public bool IsDeleted { get; set;    }

    public DateTimeOffset DeletedAtUtc { get; set; }
}
