﻿using System.Linq;

using APSS.Domain.Services;

using Xunit;

namespace APSS.Tests.Infrastructure.Services;

public abstract class RandomGeneratorServiceTests<T> where T : IRandomGeneratorService, new()
{
    #region Fields

    private const int TEST_STRING_SIZE = 0xff;

    private readonly IRandomGeneratorService _rndSvc = new T();

    #endregion Fields

    #region Public Methods

    [Fact]
    public void AlphaFact()
    {
        var str = _rndSvc.NextString(TEST_STRING_SIZE, RandomStringOptions.Alpha);

        Assert.Equal(TEST_STRING_SIZE, str.Length);
        Assert.True(str.All(char.IsLetter));
    }

    [Fact]
    public void AlphaNumFact()
    {
        var str = _rndSvc.NextString(TEST_STRING_SIZE, RandomStringOptions.AlphaNumeric);

        Assert.Equal(TEST_STRING_SIZE, str.Length);
        Assert.True(str.All(c => char.IsLetterOrDigit(c)));
    }

    [Fact]
    public void Float32Fact()
    {
        const float min = 1.0f;
        const float max = 100.0f;

        var value = _rndSvc.NextFloat32(min, max);

        Assert.True(value >= min);
        Assert.True(value < max);

        value = _rndSvc.NextFloat32(-max, -min);

        Assert.True(value >= -max);
        Assert.True(value < -min);
    }

    [Fact]
    public void Float64Fact()
    {
        const double min = 1.0;
        const double max = 100.0;

        var value = _rndSvc.NextFloat64(min, max);

        Assert.True(value >= min);
        Assert.True(value < max);

        value = _rndSvc.NextFloat64(-max, -min);

        Assert.True(value >= -max);
        Assert.True(value < -min);
    }

    [Fact]
    public void Int16Fact()
    {
        const short min = 1;
        const short max = 100;

        var value = _rndSvc.NextInt16(min, max);

        Assert.True(value >= min);
        Assert.True(value < max);

        value = _rndSvc.NextInt16(-max, -min);

        Assert.True(value >= -max);
        Assert.True(value < -min);
    }

    [Fact]
    public void Int32Fact()
    {
        const int min = 1;
        const int max = 100;

        var value = _rndSvc.NextInt32(min, max);

        Assert.True(value >= min);
        Assert.True(value < max);

        value = _rndSvc.NextInt32(-max, -min);

        Assert.True(value >= -max);
        Assert.True(value < -min);
    }

    [Fact]
    public void Int64Fact()
    {
        const long min = 1L;
        const long max = 100L;

        var value = _rndSvc.NextInt64(min, max);

        Assert.True(value >= min);
        Assert.True(value < max);

        value = _rndSvc.NextInt64(-max, -min);

        Assert.True(value >= -max);
        Assert.True(value < -min);
    }

    [Fact]
    public void LowerAlphaFact()
    {
        var str = _rndSvc.NextString(
            TEST_STRING_SIZE,
            RandomStringOptions.Alpha | RandomStringOptions.Lowercase);

        Assert.Equal(TEST_STRING_SIZE, str.Length);
        Assert.True(str.All(char.IsLower));
    }

    [Fact]
    public void MixedAlphaFact()
    {
        var str = _rndSvc.NextString(
            TEST_STRING_SIZE,
            RandomStringOptions.Alpha | RandomStringOptions.Mixedcase);

        Assert.Equal(TEST_STRING_SIZE, str.Length);
        Assert.True(str.All(c => char.IsUpper(c) || char.IsLower(c)));
    }

    [Fact]
    public void MixedFact()
    {
        var str = _rndSvc.NextString(TEST_STRING_SIZE, RandomStringOptions.Mixed);

        Assert.Equal(TEST_STRING_SIZE, str.Length);
    }

    [Fact]
    public void NumOnlyFact()
    {
        var str = _rndSvc.NextString(TEST_STRING_SIZE, RandomStringOptions.Numeric);

        Assert.Equal(TEST_STRING_SIZE, str.Length);
        Assert.True(str.All(char.IsNumber));
    }

    [Fact]
    public void SymbolFact()
    {
        var str = _rndSvc.NextString(TEST_STRING_SIZE, RandomStringOptions.Symbol);

        Assert.Equal(TEST_STRING_SIZE, str.Length);
        Assert.True(str.All(c => !char.IsLetterOrDigit(c)));
    }

    [Fact]
    public void UpperAlphaFact()
    {
        var str = _rndSvc.NextString(
            TEST_STRING_SIZE,
            RandomStringOptions.Alpha | RandomStringOptions.Uppercase);

        Assert.Equal(TEST_STRING_SIZE, str.Length);
        Assert.True(str.All(char.IsUpper));
    }

    #endregion Public Methods
}
