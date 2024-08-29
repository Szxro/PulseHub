using PulseHub.Domain.Entities;

namespace PulseHub.Domain.Contracts;

public interface ITokenService
{
    string GenerateToken(User user, double lifeTime = 10);

    string GenerateRefreshToken(int length = 32);
}
