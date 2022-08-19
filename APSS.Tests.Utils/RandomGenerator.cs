using APSS.Domain.Entities;
using APSS.Domain.Services;
using APSS.Infrastructure.Services;

namespace APSS.Tests.Utils;

public static class RandomGenerator
{
    private static readonly IRandomGeneratorService _rnd = new SecureRandomGeneratorService();

    private static readonly AccessLevel[] _accessLevels = new[]
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

    /// <summary>
    /// Generates a random string value
    /// </summary>
    /// <param name="length">The length of the string</param>
    /// <param name="opts">The options used to build the pool</param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if <see cref="RandomStringOptions.None"/> is used
    /// </exception>
    public static string NextString(
        int length,
        RandomStringOptions opts = RandomStringOptions.Alpha | RandomStringOptions.Mixedcase)
    {
        return _rnd.NextString(length, opts);
    }

    /// <summary>
    /// Generates a random 32-bit integer
    /// </summary>
    /// <param name="min">The minimum boundary</param>
    /// <param name="max">The maximum boundary</param>
    /// <returns>The generated value</returns>
    public static int NextInt32(int min = int.MinValue, int max = int.MaxValue)
        => _rnd.NextInt32(min, max);

    /// <summary>
    /// Generates a random 64-bit integer
    /// </summary>
    /// <param name="min">The minimum boundary</param>
    /// <param name="max">The maximum boundary</param>
    /// <returns>The generated value</returns>
    public static long NextInt64(long min = long.MinValue, long max = long.MaxValue)
        => _rnd.NextInt64(min, max);

    /// <summary>
    /// Generates a random 64-bit floating point number
    /// </summary>
    /// <param name="min">The minimum boundary</param>
    /// <param name="max">The maximum boundary</param>
    /// <returns>The generated value</returns>
    public static double NextFloat64(double min = double.MinValue, double max = double.MaxValue)
        => _rnd.NextFloat64(min, max);

    /// <summary>
    /// Generates a random boolean value
    /// </summary>
    /// <returns>The generated value</returns>
    public static bool NextBool() => _rnd.NextBool();

    /// <summary>
    /// Generates a random access level
    /// </summary>
    /// <param name="min">The minimum boundary</param>
    /// <param name="max">The maximum boundary</param>
    /// <returns>The generated value</returns>
    public static AccessLevel NextAccessLevel(
        AccessLevel min = AccessLevel.Farmer,
        AccessLevel max = AccessLevel.Root)
    {
        while (true)
        {
            var idx = NextInt32(0, _accessLevels.Length - 1);

            if (_accessLevels[idx].IsBelow(min) || _accessLevels[idx].IsAbove(max))
                continue;

            return _accessLevels[idx];
        }
    }
}