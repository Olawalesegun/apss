using APSS.Domain.Services.Util;

namespace APSS.Tests.Utils;

public sealed class SimpleRandomGeneratorService : BasicRandomStringGeneratorService
{
    #region Fields

    private readonly Random _rnd = new(DateTime.Now.Millisecond);

    #endregion Fields

    #region Public Methods

    /// <inheritdoc/>
    public override IEnumerable<byte> NextBytes(int length)
    {
        var buffer = new byte[length];

        _rnd.NextBytes(buffer);

        return buffer;
    }

    /// <inheritdoc/>
    public override double NextFloat64(double min = double.MinValue, double max = double.MaxValue)
        => _rnd.NextDouble() * (max - min) + min;

    /// <inheritdoc/>
    public override long NextInt64(long min = long.MinValue, long max = long.MaxValue)
        => _rnd.NextInt64(min, max);

    #endregion Public Methods
}