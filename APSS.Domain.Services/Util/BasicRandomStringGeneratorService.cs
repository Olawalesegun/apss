using System.Text;

using APSS.Domain.Entities;

namespace APSS.Domain.Services.Util;

public abstract class BasicRandomStringGeneratorService : IRandomGeneratorService
{
    /// <inheritdoc/>
    public abstract long NextInt64(long min = long.MinValue, long max = long.MaxValue);

    /// <inheritdoc/>
    public abstract double NextFloat64(double min = double.MinValue, double max = double.MaxValue);

    /// <inheritdoc/>
    public abstract IEnumerable<byte> NextBytes(int length);

    /// <inheritdoc/>
    public virtual short NextInt16(short min = short.MinValue, short max = short.MaxValue)
        => (short)NextInt32(min, max);

    /// <inheritdoc/>
    public virtual int NextInt32(int min = int.MinValue, int max = int.MaxValue)
        => (int)NextInt64(min, max);

    /// <inheritdoc/>
    public virtual float NextFloat32(float min = float.MinValue, float max = float.MaxValue)
        => (float)NextFloat64(min, max);

    /// <inheritdoc/>
    public virtual bool NextBool() => NextInt32(0, 1) == 1;

    /// <inheritdoc/>
    public virtual string NextString(int length, RandomStringOptions opts = RandomStringOptions.Mixed)
    {
        var pool = GenerateStringPool(opts);

        return new string(Enumerable
            .Range(0, length)
            .Select(i => pool[NextInt32(0, pool.Length - 1)])
            .ToArray());
    }

    /// <inheritdoc/>
    public AccessLevel NextAccessLevel(
        AccessLevel min = AccessLevel.Farmer,
        AccessLevel max = AccessLevel.Root)
    {
        var accessLevels = new[]
        {
            AccessLevel.Farmer,
            AccessLevel.Group,
            AccessLevel.Village,
            AccessLevel.District,
            AccessLevel.Directorate,
            AccessLevel.Governorate,
            AccessLevel.Presedint,
            AccessLevel.Root,
        };

        while (true)
        {
            var idx = NextInt32(0, accessLevels.Length - 1);

            if (accessLevels[idx].IsBelow(min) || accessLevels[idx].IsAbove(max))
                continue;

            return accessLevels[idx];
        }
    }

    /// <summary>
    /// Generates the random string generation pool
    /// </summary>
    /// <param name="opts">Options to generate the pool with</param>
    /// <returns>Generated pool</returns>
    private static string GenerateStringPool(RandomStringOptions opts)
    {
        const string LOWERCASE_ALPHA = "abcdefghijklmnopqrstuvwxyz";
        const string UPPERCASE_ALPHA = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string NUMBERS = "1234567890";
        const string SYMBOLS = "`~!@#$%^&*()-_=+\"\\/?.>,<";

        var poolBuilder = new StringBuilder();

        if (opts.HasFlag(RandomStringOptions.Alpha))
        {
            if (!opts.HasFlag(RandomStringOptions.Lowercase) &&
                !opts.HasFlag(RandomStringOptions.Uppercase))
            {
                poolBuilder.Append(LOWERCASE_ALPHA);
            }
            else
            {
                if (opts.HasFlag(RandomStringOptions.Lowercase))
                    poolBuilder.Append(LOWERCASE_ALPHA);

                if (opts.HasFlag(RandomStringOptions.Uppercase))
                    poolBuilder.Append(UPPERCASE_ALPHA);
            }
        }

        if (opts.HasFlag(RandomStringOptions.Numeric))
            poolBuilder.Append(NUMBERS);

        if (opts.HasFlag(RandomStringOptions.Symbol))
            poolBuilder.Append(SYMBOLS);

        return poolBuilder.ToString();
    }
}