using PulseHub.SharedKernel;

namespace PulseHub.Domain.Entities;

public class Application : Entity
{
    public Application()
    {
        AccessKeys = new HashSet<AccessKey>();
    }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string ProviderApplicationId { get; set; } = string.Empty;

    public int UserId { get; set; }

    public User User { get; set; } = null!;

    public int ProviderId { get; set; }

    public Provider Provider { get; set; } = null!;

    public ICollection<AccessKey> AccessKeys { get; set; }
}