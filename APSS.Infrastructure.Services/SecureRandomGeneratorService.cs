using System.Security.Cryptography;

using APSS.Domain.Entities;
using APSS.Domain.Services;

namespace APSS.Infrastructure.Services;

public sealed class SecureRandomGeneratorService : IRandomGeneratorService
{
    /// <inheritdoc/>
    public short NextInt16(short min = short.MinValue, short max = short.MaxValue)
        => (short)RandomNumberGenerator.GetInt32(min, max);

    /// <inheritdoc/>
    public int NextInt32(int min = int.MinValue, int max = int.MaxValue)
        => RandomNumberGenerator.GetInt32(min, max);

    /// <inheritdoc/>
    public long NextInt64(long min = long.MinValue, long max = long.MaxValue)
    {
        if (min >= max)
            throw new ArgumentException("max bound cannot be lower or equal to min bound");

        var randomBytes = NextBytes(sizeof(long)).ToArray();
        var value = BitConverter.ToInt64(randomBytes);

        return Math.Abs(value % (max - min)) + min;
    }

    /// <inheritdoc/>
    public float NextFloat32(float min = float.MinValue, float max = float.MaxValue)
        => (float)checked(NextFloat64(min, max));

    /// <inheritdoc/>
    public double NextFloat64(double min = double.MinValue, double max = double.MaxValue)
    {
        return (1.0 / NextInt32(2)) * (max - min) + min;
    }

    /// <inheritdoc/>
    public bool NextBool() => NextInt32(0, 1) == 1;

    /// <inheritdoc/>
    public IEnumerable<byte> NextBytes(int length)
        => RandomNumberGenerator.GetBytes(length);
}