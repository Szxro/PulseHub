namespace PulseHub.SharedKernel;

public class AuditableEntity
{
    public DateTimeOffset CreatedAtUtc { get; set; }

    public DateTimeOffset ModifiedAtUtc { get; set; }
}
