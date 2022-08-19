using System.Text;

namespace APSS.Domain.Services;

public interface ICryptoHashService
{
    /// <summary>
    /// Asynchronously hashes a block of data
    /// </summary>
    /// <param name="plain">The plain data to be hashed</param>
    /// <param name="salt">The salt used for hashing</param>
    /// <returns>The generated hash</returns>
    Task<byte[]> HashAsync(byte[] plain, byte[] salt);

    /// <summary>
    /// Asynchronously verifies a hash
    /// </summary>
    /// <param name="hash">The hash to be verified</param>
    /// <param name="plain">The plain data of the hash</param>
    /// <param name="salt">The salt used for hashing</param>
    /// <returns>True if the hash is valid, false otherwise</returns>
    Task<bool> VerifyAsync(byte[] hash, byte[] plain, byte[] salt);

    /// <summary>
    /// Asynchronously hashes a string
    /// </summary>
    /// <param name="plain">The string to be hashed</param>
    /// <param name="salt">The salt used for hashing</param>
    /// <returns>The generated hash encoded in base64</returns>
    async Task<string> HashAsync(string plain, string salt)
        => Convert.ToBase64String(await HashAsync(Encoding.UTF8.GetBytes(plain), Convert.FromBase64String(salt)));

    /// <summary>
    /// Asynchronously verifies a hash
    /// </summary>
    /// <param name="hash">The base64-encoded hash to be verified</param>
    /// <param name="plain">The plain string of the hash</param>
    /// <param name="salt">The salt used for hashing</param>
    /// <returns>True if the hash is valid, false otherwise</returns>
    Task<bool> VerifyAsync(string hash, string plain, string salt)
        => VerifyAsync(
            Convert.FromBase64String(hash),
            Encoding.UTF8.GetBytes(plain),
            Convert.FromBase64String(salt));
}