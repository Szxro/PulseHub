using PulseHub.SharedKernel;

namespace PulseHub.Domain.Entities;

public class RefreshToken : Entity
{
    public string Value { get; set; } = string.Empty;

    public bool IsRevoked { get; set; }

    public bool IsUsed { get; set; }

    public DateTime ExpirationDate { get; } = DateTime.Now.AddHours(1);

    public int UserId { get; set; }

    public User User { get; set; } = null!;
}