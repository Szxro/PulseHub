using PulseHub.Domain.Entities;

namespace PulseHub.Domain.Contracts;

public interface IAvatarRepository : IRepositoryWriter<Avatar>
{
    Task<Avatar?> GetUserAvatarByUsernameAsync(string username, CancellationToken cancellationToken = default);
}
