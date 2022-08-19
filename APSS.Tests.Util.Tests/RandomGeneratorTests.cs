using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using APSS.Domain.Entities;
using APSS.Domain.Services;
using APSS.Tests.Utils;

namespace APSS.Tests.Util.Tests;

[TestClass]
public class RandomGeneratorTests
{
    #region Constants

    private const int _testStringSize = 0xff;

    #endregion Constants

    #region Tests

    [TestMethod]
    public void NumOnlyTest()
    {
        var str = RandomGenerator.NextString(_testStringSize, RandomStringOptions.Numeric);

        Assert.AreEqual(str.Length, _testStringSize);
        Assert.IsTrue(str.All(char.IsNumber));
    }

    [TestMethod]
    public void AlphaTest()
    {
        var str = RandomGenerator.NextString(_testStringSize, RandomStringOptions.Alpha);

        Assert.AreEqual(str.Length, _testStringSize);
        Assert.IsTrue(str.All(char.IsLetter));
    }

    [TestMethod]
    public void LowerAlphaTest()
    {
        var str = RandomGenerator.NextString(
            _testStringSize,
            RandomStringOptions.Alpha | RandomStringOptions.Lowercase);

        Assert.AreEqual(str.Length, _testStringSize);
        Assert.IsTrue(str.All(char.IsLower));
    }

    [TestMethod]
    public void UpperAlphaTest()
    {
        var str = RandomGenerator.NextString(
            _testStringSize,
            RandomStringOptions.Alpha | RandomStringOptions.Uppercase);

        Assert.AreEqual(str.Length, _testStringSize);
        Assert.IsTrue(str.All(char.IsUpper));
    }

    [TestMethod]
    public void MixedAlphaTest()
    {
        var str = RandomGenerator.NextString(
            _testStringSize,
            RandomStringOptions.Alpha | RandomStringOptions.Mixedcase);

        Assert.AreEqual(str.Length, _testStringSize);
        Assert.IsTrue(str.All(c => char.IsUpper(c) || char.IsLower(c)));
    }

    [TestMethod]
    public void AlphaNumTest()
    {
        var str = RandomGenerator.NextString(_testStringSize, RandomStringOptions.AlphaNumeric);

        Assert.AreEqual(str.Length, _testStringSize);
        Assert.IsTrue(str.All(c => char.IsLetterOrDigit(c)));
    }

    [TestMethod]
    public void SymbolTest()
    {
        var str = RandomGenerator.NextString(_testStringSize, RandomStringOptions.Symbol);

        Assert.AreEqual(str.Length, _testStringSize);
        Assert.IsTrue(str.All(c => !char.IsLetterOrDigit(c)));
    }

    [TestMethod]
    public void MixedTest()
    {
        var str = RandomGenerator.NextString(_testStringSize, RandomStringOptions.Mixed);

        Assert.AreEqual(str.Length, _testStringSize);
    }

    [TestMethod]
    public void IntTest()
    {
        const int min = 1;
        const int max = 100;

        var value = RandomGenerator.NextInt32(min, max);

        Assert.IsTrue(value >= min);
        Assert.IsTrue(value < max);
    }

    [TestMethod]
    public void LongTest()
    {
        const long min = 1L;
        const long max = 100L;

        var value = RandomGenerator.NextInt64(min, max);

        Assert.IsTrue(value >= min);
        Assert.IsTrue(value < max);
    }

    [TestMethod]
    public void DoubleTest()
    {
        const double min = 1.0;
        const double max = 100.0;

        var value = RandomGenerator.NextFloat64(min, max);

        Assert.IsTrue(value >= min);
        Assert.IsTrue(value < max);
    }

    [TestMethod]
    public void AccessLevelTest()
    {
        var min = RandomGenerator.NextAccessLevel(AccessLevel.Farmer, AccessLevel.Directorate);
        var max = RandomGenerator.NextAccessLevel(AccessLevel.Governorate, AccessLevel.Root);

        Assert.IsTrue(min.IsAboveOrEqual(AccessLevel.Farmer));
        Assert.IsTrue(min.IsBelowOrEqual(AccessLevel.Directorate));

        Assert.IsTrue(max.IsAboveOrEqual(AccessLevel.Governorate));
        Assert.IsTrue(max.IsBelowOrEqual(AccessLevel.Root));

        var value = RandomGenerator.NextAccessLevel(min, max);

        Assert.IsTrue(max.IsAboveOrEqual(min));
        Assert.IsTrue(max.IsBelowOrEqual(max));
    }

    #endregion Tests
}