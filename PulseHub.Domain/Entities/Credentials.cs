using PulseHub.SharedKernel;

namespace PulseHub.Domain.Entities;

public class Credentials : Entity
{
    public string HashValue { get; set; } = string.Empty;

    public string SaltValue { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public decimal Version { get; set; } = 1.0M;

    public int UserId { get; set; }

    public User User { get; set; } = null!;
}
