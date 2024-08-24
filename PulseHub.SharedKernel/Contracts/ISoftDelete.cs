namespace PulseHub.SharedKernel.Contracts;

public interface ISoftDelete
{
    public bool IsDeleted { get; set; }

    public DateTimeOffset DeletedAtUtc { get; set; }
}
