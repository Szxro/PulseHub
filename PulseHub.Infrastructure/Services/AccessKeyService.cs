using Microsoft.Extensions.Options;
using PulseHub.Domain.Contracts;
using PulseHub.Infrastructure.Options.Aes;
using System.Security.Cryptography;
using System.Text;

namespace PulseHub.Infrastructure.Services;

public class AccessKeyService : IAccessKeyService
{
    private static readonly char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();

    private readonly AesOptions _options;

    public AccessKeyService(IOptions<AesOptions> options)
    {
        _options = options.Value;
    }

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
    public string DecryptAccessKey(string cipherText)
    {
        // Converting from a string to byte[]
        byte[] buffer = Convert.FromBase64String(cipherText);

        // Initialization vector (need to be the same vector to encrypt and decrypt)
        byte[] iv = new byte[16];

        using (SymmetricAlgorithm aes = Aes.Create())
        {
            aes.Mode = CipherMode.CBC;
            aes.Key = GenerateAesKey();
            aes.IV = iv;

            using (ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
            {
                // In this case is going to create a stream in memory base on the buffer
                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    // In this case CryptoStreamMode is going to be Read (read the memory stream)
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader(cryptoStream))
                        {
                            // Reads all the characters in a stream  
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }
    }

    public string EncryptAccessKey(string content)
    {
        byte[] encrypted;

        // iv => Initialization vector (need to be the same vector to encrypt and decrypt)
        byte[] iv = new byte[16];

        // Advanced Encryption Standard (2001)
        using (SymmetricAlgorithm aes = Aes.Create())
        {
            // Mode use to encripted the content different to not be the same
            aes.Mode = CipherMode.CBC;

            // Secret Key that is going to be use in the encrypt and decrypt process
            aes.Key = GenerateAesKey();

            // Its better to use a different vector to encrypted differently the key
            aes.IV = iv;

            using (ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
            {
                // Creating a stream who is going to be store in memory
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    // Stream that is main purpose is cryptograhic transformations
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                        {
                            // Writing the string content to the cryptoStream
                            streamWriter.Write(content);
                        }

                        // Adding the result to the encripted variable
                        encrypted = memoryStream.ToArray();
                    }
                }
            }
        }

        // Converting the array of bites to its equivalent in a string format
        return Convert.ToBase64String(encrypted);
    }

    private byte[] GenerateAesKey()
    {
        byte[] buffer = Encoding.UTF8.GetBytes(_options.PrivateKey);

        using SHA256 hash = SHA256.Create();

        return hash.ComputeHash(buffer);
    }
}
