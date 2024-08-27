using PulseHub.Domain.Entities;
using PulseHub.Domain.ValueObjects;

namespace PulseHub.Domain.Contracts;

public interface IUserRepository : IRepositoryWriter<User>
{
    Task<bool> IsEmailUnique(Email email);

    Task<bool> IsUserNameUnique(string username);
}
