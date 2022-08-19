using System.Text;

namespace APSS.Domain.Services;

public interface IRandomGeneratorService
{
    /// <summary>
    /// Generates a random 16-bit integer
    /// </summary>
    /// <param name="min">The minimum boundary</param>
    /// <param name="max">The maximum boundary</param>
    /// <returns>The generated value</returns>
    public short NextInt16(short min = short.MinValue, short max = short.MaxValue);

    /// <summary>
    /// Generates a random 32-bit integer
    /// </summary>
    /// <param name="min">The minimum boundary</param>
    /// <param name="max">The maximum boundary</param>
    /// <returns>The generated value</returns>
    public int NextInt32(int min = int.MinValue, int max = int.MaxValue);

    /// <summary>
    /// Generates a random 64-bit integer
    /// </summary>
    /// <param name="min">The minimum boundary</param>
    /// <param name="max">The maximum boundary</param>
    /// <returns>The generated value</returns>
    public long NextInt64(long min = long.MinValue, long max = long.MaxValue);

    /// <summary>
    /// Generates a random 32-bit floating point number
    /// </summary>
    /// <param name="min">The minimum boundary</param>
    /// <param name="max">The maximum boundary</param>
    /// <returns>The generated value</returns>
    public float NextFloat32(float min = float.MinValue, float max = float.MaxValue);

    /// <summary>
    /// Generates a random 64-bit floating point number
    /// </summary>
    /// <param name="min">The minimum boundary</param>
    /// <param name="max">The maximum boundary</param>
    /// <returns>The generated value</returns>
    public double NextFloat64(double min = double.MinValue, double max = double.MaxValue);

    /// <summary>
    /// Generates a random boolean value
    /// </summary>
    /// <returns>The generated value</returns>
    public bool NextBool();

    /// <summary>
    /// Generates a random sequence of bytes
    /// </summary>
    /// <param name="length">The length of the sequence to be generated</param>
    /// <returns>The generated sequence</returns>
    public IEnumerable<byte> NextBytes(int length);

    /// <summary>
    /// Generates a random string value
    /// </summary>
    /// <param name="length">The length of the string</param>
    /// <param name="opts">The options used to build the pool</param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if <see cref="RandomStringOptions.None"/> is used
    /// </exception>
    string NextString(int length, RandomStringOptions opts = RandomStringOptions.Mixed)
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

/// <summary>
/// An enum to describe the options that can be used for string generation
/// </summary>
[Flags]
public enum RandomStringOptions : int
{
    Alpha = 1 << 0,
    Numeric = 1 << 1,
    Symbol = 1 << 3,
    AlphaNumeric = Alpha | Numeric,
    Lowercase = 1 << 4,
    Uppercase = 1 << 5,
    Mixedcase = Lowercase | Uppercase,
    Mixed = Alpha | Numeric | Lowercase | Uppercase | Symbol,
};