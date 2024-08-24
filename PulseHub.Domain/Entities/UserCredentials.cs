using PulseHub.SharedKernel;

namespace PulseHub.Domain.Entities;

public class UserCredentials : IntermediaryEntity
{
    public int UserId { get; set; }

    public User User { get; set; } = null!;

    public int CredentialsId { get; set; }

    public Credentials Credentials { get; set; } = null!;
}
