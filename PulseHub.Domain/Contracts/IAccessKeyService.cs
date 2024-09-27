namespace PulseHub.Domain.Contracts;

public interface IAccessKeyService
{
    string GenerateUniqueAccessKey(int length = 10);

    string EncryptAccessKey(string content);

    string DecryptAccessKey(string cipherText);
}
