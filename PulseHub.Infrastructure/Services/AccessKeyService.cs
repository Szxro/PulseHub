using PulseHub.Domain.Contracts;
using System.Security.Cryptography;
using System.Text;

namespace PulseHub.Infrastructure.Services;

public class AccessKeyService : IAccessKeyService
{
    private static readonly char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();

    public string GenerateUniqueAccessKey(int length = 10)
    {
        byte[] buffer = new byte[4 * length];

        using RandomNumberGenerator rng = RandomNumberGenerator.Create();

        // Fill the array of bytes with random sequence of values
        rng.GetBytes(buffer);

        StringBuilder builder = new StringBuilder(length);

        for (int i = 0; i < length; i++)
        {
            // Getting a random uint from the buffer at a specify position
            uint randomNumber = BitConverter.ToUInt32(buffer, i * 4);

            long index = randomNumber % chars.Length; // calculating the index 

            builder.Append(chars[index]);
        }

        return builder.ToString();
    }
}
