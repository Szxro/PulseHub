using PulseHub.Domain.ValueObjects;
using PulseHub.SharedKernel;

namespace PulseHub.Domain.Entities;

public class User : Entity
{
    public User()
    {
        UserCredentials = new HashSet<UserCredentials>();
        Applications = new HashSet<Application>();
    }

    public string FirstName { get; set; } = string.Empty;

    public string Lastname { get; set; } = string.Empty;

    public string Username { get; set; } = string.Empty;

    public Email Email { get; set; } = null!;

    public bool LockOutEnable { get; set; } = true;

    public DateTime LockOutEnd { get; set; } = new DateTime(1999, 01, 01, 01, 00, 00);

    public int AccessFailedCount { get; set; } = 0;

    public ICollection<UserCredentials> UserCredentials { get; set; }

    public ICollection<Application> Applications { get; set; }
}
