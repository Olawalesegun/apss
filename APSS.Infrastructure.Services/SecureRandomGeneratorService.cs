using System;
using System.Security.Cryptography;
using System.Text;

using APSS.Domain.Services;

namespace APSS.Infrastructure.Services;

public sealed class SecureRandomGeneratorService : IRandomGeneratorService
{
    private const string LOWERCASE_ALPHA = "abcdefghijklmnopqrstuvwxyz";
    private const string UPPERCASE_ALPHA = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const string NUMBERS = "1234567890";
    private const string SYMBOLS = "`~!@#$%^&*()-_=+\"\\/?.>,<";

    /// <summary>
    /// Gets a singleton instance of this class
    /// </summary>
    public static readonly IRandomGeneratorService Instance = new SecureRandomGeneratorService();

    /// <inheritdoc/>
    public short NextInt16(short min = short.MinValue, short max = short.MaxValue)
        => min == max ? min : (short)RandomNumberGenerator.GetInt32(min, max);

    /// <inheritdoc/>
    public int NextInt32(int min = int.MinValue, int max = int.MaxValue)
        => min == max ? min : RandomNumberGenerator.GetInt32(min, max);

    /// <inheritdoc/>
    public long NextInt64(long min = long.MinValue, long max = long.MaxValue)
    {
        long range = max - min;
        long val;

        do
        {
            val = BitConverter.ToInt64(NextBytes(sizeof(long)).ToArray());
        } while (val > long.MaxValue - ((long.MaxValue % range) + 1) % range);

        return (val % range) + min;
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

    /// <inheritdoc/>
    public string NextString(int length, RandomStringOptions opts = RandomStringOptions.Mixed)
    {
        var pool = GenerateStringPool(opts);

        return new string(Enumerable
            .Range(0, length)
            .Select(i => pool[NextInt32(0, pool.Length - 1)])
            .ToArray());
    }

    /// <summary>
    /// Generates the random string generation pool
    /// </summary>
    /// <param name="opts">Options to generate the pool with</param>
    /// <returns>Generated pool</returns>
    private string GenerateStringPool(RandomStringOptions opts)
    {
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