using PulseHub.SharedKernel;

namespace PulseHub.Domain.Entities;

public class Credentials : Entity
{
    public Credentials()
    {
        UserCredentials = new HashSet<UserCredentials>();
    }

    public string HashValue { get; set; } = string.Empty;

    public string SaltValue { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public ICollection<UserCredentials> UserCredentials { get; set; }
}
