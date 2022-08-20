using APSS.Domain.Services;
using APSS.Infrastructure.Services;

using Xunit;

namespace APSS.Tests.Infrastructure.Services;

public sealed class AppSettingsConfigurationServiceTests
{
    private readonly IRandomGeneratorService _rndSvc;
    private readonly IConfigurationService _configSvc;

    public AppSettingsConfigurationServiceTests()
    {
        _rndSvc = new SecureRandomGeneratorService();
        _configSvc = new AppSettingsConfigurationService();
    }

    [Fact]
    public void IndexerFact()
    {
        var key = _rndSvc.NextString(0x7f);
        var value = _rndSvc.NextString(0x7f);

        var setValue = _configSvc[key] = value;

        Assert.True(_configSvc.HasKey(key));
        Assert.Equal(value, setValue);
        Assert.Equal(value, _configSvc[key]);
    }

    [Fact]
    public void StringFact()
    {
        var key = _rndSvc.NextString(0x7f);
        var value = _rndSvc.NextString(0x7f);

        _configSvc.Set(key, value);

        Assert.True(_configSvc.HasKey(key));
        Assert.Equal(value, _configSvc[key]);
    }

    [Fact]
    public void IntFact()
    {
        var key = _rndSvc.NextString(0x7f);
        var value = _rndSvc.NextInt32();

        _configSvc.Set(key, value);

        Assert.True(_configSvc.HasKey(key));
        Assert.Equal(value, _configSvc.GetInt(key));
    }

    [Fact]
    public void DoubleFact()
    {
        var key = _rndSvc.NextString(0x7f);
        var value = _rndSvc.NextFloat64();

        _configSvc.Set(key, value);

        Assert.True(_configSvc.HasKey(key));
        Assert.Equal(value, _configSvc.GetDouble(key));
    }

    [Fact]
    public void BoolFact()
    {
        var key = _rndSvc.NextString(0x7f);
        var value = _rndSvc.NextBool();

        _configSvc.Set(key, value);

        Assert.True(_configSvc.HasKey(key));
        Assert.Equal(value, _configSvc.GetBool(key));
    }

    [Fact]
    public void StringDefaultFact()
    {
        var key = _rndSvc.NextString(0x7f);
        var defaultValue = _rndSvc.NextString(0x7f);

        Assert.False(_configSvc.HasKey(key));
        Assert.Equal("default", _configSvc.Get(key, defaultValue));
    }

    [Fact]
    public void IntDefaultFact()
    {
        var key = _rndSvc.NextString(0x7f);
        var defaultValue = _rndSvc.NextInt32();

        Assert.False(_configSvc.HasKey(key));
        Assert.Equal(defaultValue, _configSvc.GetInt(key, defaultValue));
    }

    [Fact]
    public void DoubleDefaultFact()
    {
        var key = _rndSvc.NextString(0x7f);
        var defaultValue = _rndSvc.NextFloat64();

        Assert.False(_configSvc.HasKey(key));
        Assert.Equal(defaultValue, _configSvc.GetDouble(key, defaultValue));
    }

    [Fact]
    public void BoolDefaultFact()
    {
        var key = _rndSvc.NextString(0x7f);
        var defaultValue = _rndSvc.NextBool();

        Assert.False(_configSvc.HasKey(key));
        Assert.Equal(defaultValue, _configSvc.GetBool(key, defaultValue));
    }
}