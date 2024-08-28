namespace PulseHub.Domain.Contracts;

public interface IAccessKeyService
{
    string GenerateUniqueAccessKey(int length = 10);
}
