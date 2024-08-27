namespace PulseHub.SharedKernel.Contracts;

public interface ISoftDelete
{
    public bool IsDeleted { get; set; }

    public DateTime DeletedAtUtc { get; set; }
}
