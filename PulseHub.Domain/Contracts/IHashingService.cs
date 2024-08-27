namespace PulseHub.Domain.Contracts;

public interface IHashingService
{
    string GetHash(string password,out byte[] salt);

    bool VerifyHash(string password, string currentHash, string currentSalt);
}
