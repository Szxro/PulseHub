using PulseHub.SharedKernel;

namespace PulseHub.Domain.Entities;

public class RefreshToken : Entity
{
    public string Value { get; set; } = string.Empty;

    public bool IsRevoked { get; set; } = false;

    public bool IsExpired { get; set; } = false;

    public bool IsUsed { get; set; } = false;

    public DateTime ExpirationDate { get; set; } = DateTime.Now.AddMinutes(45);

    public int UserId { get; set; }

    public User User { get; set; } = null!;
}