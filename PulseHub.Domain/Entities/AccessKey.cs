using PulseHub.SharedKernel;

namespace PulseHub.Domain.Entities;

public class AccessKey : Entity
{
    public string EncryptedKey { get; set; } = string.Empty;

    public int ApplicationId { get; set; }

    public Application Application { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTimeOffset LastUsed { get; set; }
}
