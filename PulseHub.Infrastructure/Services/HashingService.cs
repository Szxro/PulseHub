using Microsoft.Extensions.Options;
using PulseHub.Domain.Contracts;
using PulseHub.Infrastructure.Options.Hashing;
using System.Security.Cryptography;

namespace PulseHub.Infrastructure.Services;

public class HashingService : IHashingService
{
    private readonly HashingOptions _options;

    private static readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA512;

    public HashingService(IOptions<HashingOptions> options)
    {
        _options = options.Value;
    }
    public string GetHash(string password, out byte[] salt)
    {
        // Generating a salt with random bytes
        salt = RandomNumberGenerator.GetBytes(_options.SaltSize);

        // Commonly use for passwords 
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
            password,
            salt,
            _options.Iterations,
            Algorithm,
            _options.HashSize);

        // Converting from byte[] to string (return its representation in string)
        return Convert.ToHexString(hash);
    }

    public bool VerifyHash(string password, string currentHash, string currentSalt)
    {
        // Converting from string to byte[] (return its representation in byte[])
        byte[] salt = Convert.FromHexString(currentSalt);

        byte[] hash = Convert.FromHexString(currentHash);

        // Re-creating the hash
        byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(
            password,
            salt,
            _options.Iterations,
            Algorithm,
            _options.HashSize);

        // Comparing both base on the length
        return CryptographicOperations.FixedTimeEquals(hash,inputHash); // recommend way of doing to prevent timing attacks
    }
}