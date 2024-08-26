using PulseHub.SharedKernel.Contracts;

namespace PulseHub.SharedKernel;

public class IntermediaryEntity : AuditableEntity, IEntity
{
    public int Id { get; set; }
}
