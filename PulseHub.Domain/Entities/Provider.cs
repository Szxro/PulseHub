using PulseHub.SharedKernel;

namespace PulseHub.Domain.Entities;

public class Provider : Entity
{
    public Provider()
    {
        Applications = new HashSet<Application>();
    }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public ICollection<Application> Applications { get; set; }
}
