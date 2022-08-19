using APSS.Domain.Services;

using Konscious.Security.Cryptography;

namespace APSS.Infrastructure.Services;

public sealed class Argon2iCryptoHashService : ICryptoHashService
{
    private const int HASH_PARALLELISM_DEGREE = 16;
    private const int HASH_MEMORY_SIZE = 8192;
    private const int HASH_ITERATION_COUNT = 48;

    public const int HASH_BYTE_COUNT = 128;

    /// <inheritdoc/>
    public Task<byte[]> HashAsync(byte[] plain, byte[] salt)
    {
        var argon2 = new Argon2i(plain)
        {
            DegreeOfParallelism = HASH_PARALLELISM_DEGREE,
            MemorySize = HASH_MEMORY_SIZE,
            Iterations = HASH_ITERATION_COUNT,
            Salt = salt,
        };

        return argon2.GetBytesAsync(HASH_BYTE_COUNT);
    }

    /// <inheritdoc/>
    public async Task<bool> VerifyAsync(byte[] hash, byte[] plain, byte[] salt)
        => hash.SequenceEqual(await HashAsync(plain, salt));
}