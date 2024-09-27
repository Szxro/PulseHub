namespace PulseHub.Domain.Contracts;

public interface ICurrentUserService
{
    (string? username, string? email) GetCurrentUser();
}
