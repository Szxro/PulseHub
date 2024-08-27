using PulseHub.Domain.ValueObjects;
using PulseHub.SharedKernel;

namespace PulseHub.Domain.Entities;

public class User : Entity
{
    public User()
    {
        Applications = new HashSet<Application>();
        Credentials = new HashSet<Credentials>();
    }

    public string FirstName { get; set; } = string.Empty;

    public string Lastname { get; set; } = string.Empty;

    public string Username { get; set; } = string.Empty;

    public Email Email { get; set; } = null!;

    public bool LockOutEnable { get; set; } = true;

    public bool IsEmailVerified { get; set; } = false;

    public DateTime LockOutEnd { get; set; } = DateTime.MinValue;

    public int AccessFailedCount { get; set; } = 0;

    public ICollection<Credentials> Credentials { get; private set; }

    public ICollection<Application> Applications { get; set; }

    public void AddCredentials(Credentials credentials)
    {
        Credentials.Add(credentials);
    }
}
