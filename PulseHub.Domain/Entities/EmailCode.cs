using PulseHub.SharedKernel;

namespace PulseHub.Domain.Entities;

public class EmailCode : Entity
{
    public string Code { get; set; } = string.Empty;

    public bool IsInvalid { get; set; }

    public bool IsVerified { get; set; }

    public bool IsExpired { get; set; }

    public int UserId { get; set; }

    public User? User { get; set; } 
}
