using System.Security.Cryptography;

using APSS.Domain.Entities;
using APSS.Domain.Services;
using APSS.Domain.Services.Util;

namespace APSS.Infrastructure.Services;

public sealed class SecureRandomGeneratorService : BasicRandomStringGeneratorService
{
    /// <inheritdoc/>
    public override int NextInt32(int min = int.MinValue, int max = int.MaxValue)
        => RandomNumberGenerator.GetInt32(min, max);

    /// <inheritdoc/>
    public override long NextInt64(long min = long.MinValue, long max = long.MaxValue)
    {
        if (min >= max)
            throw new ArgumentException("max bound cannot be lower or equal to min bound");

        var randomBytes = NextBytes(sizeof(long)).ToArray();
        var value = BitConverter.ToInt64(randomBytes);

        return Math.Abs(value % (max - min)) + min;
    }

    /// <inheritdoc/>
    public override double NextFloat64(double min = double.MinValue, double max = double.MaxValue)
    {
        if (min >= max)
            throw new ArgumentException("max bound cannot be lower or equal to min bound");

        var randomBytes = NextBytes(sizeof(long)).ToArray();
        var value = BitConverter.ToDouble(randomBytes);

        return Math.Abs(value % (max - min)) + min;
    }

    /// <inheritdoc/>
    public override IEnumerable<byte> NextBytes(int length)
        => RandomNumberGenerator.GetBytes(length);
}