namespace APSS.Domain.Services;

public interface IRandomGeneratorService
{
    /// <summary>
    /// Generates a random 16-bit integer
    /// </summary>
    /// <param name="min">The minimum boundary</param>
    /// <param name="max">The maximum boundary</param>
    /// <returns>The generated value</returns>
    short NextInt16(short min = short.MinValue, short max = short.MaxValue);

    /// <summary>
    /// Generates a random 32-bit integer
    /// </summary>
    /// <param name="min">The minimum boundary</param>
    /// <param name="max">The maximum boundary</param>
    /// <returns>The generated value</returns>
    int NextInt32(int min = int.MinValue, int max = int.MaxValue);

    /// <summary>
    /// Generates a random 64-bit integer
    /// </summary>
    /// <param name="min">The minimum boundary</param>
    /// <param name="max">The maximum boundary</param>
    /// <returns>The generated value</returns>
    long NextInt64(long min = long.MinValue, long max = long.MaxValue);

    /// <summary>
    /// Generates a random 32-bit floating point number
    /// </summary>
    /// <param name="min">The minimum boundary</param>
    /// <param name="max">The maximum boundary</param>
    /// <returns>The generated value</returns>
    float NextFloat32(float min = float.MinValue, float max = float.MaxValue);

    /// <summary>
    /// Generates a random 64-bit floating point number
    /// </summary>
    /// <param name="min">The minimum boundary</param>
    /// <param name="max">The maximum boundary</param>
    /// <returns>The generated value</returns>
    double NextFloat64(double min = double.MinValue, double max = double.MaxValue);

    /// <summary>
    /// Generates a random boolean value
    /// </summary>
    /// <returns>The generated value</returns>
    bool NextBool();

    /// <summary>
    /// Generates a random sequence of bytes
    /// </summary>
    /// <param name="length">The length of the sequence to be generated</param>
    /// <returns>The generated sequence</returns>
    IEnumerable<byte> NextBytes(int length);

    /// <summary>
    /// Generates a random string value
    /// </summary>
    /// <param name="length">The length of the string</param>
    /// <param name="opts">The options used to build the pool</param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if <see cref="RandomStringOptions.None"/> is used
    /// </exception>
    string NextString(int length, RandomStringOptions opts = RandomStringOptions.Mixed);
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