namespace PulseHub.SharedKernel;

public class AuditableEntity
{
    public DateTime CreatedAtUtc { get; set; }

    public DateTime ModifiedAtUtc { get; set; }
}
