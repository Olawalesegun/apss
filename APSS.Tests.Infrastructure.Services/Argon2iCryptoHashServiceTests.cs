using System.Text;

using APSS.Domain.Services;
using APSS.Infrastructure.Services;

using Xunit;

namespace APSS.Tests.Infrastructure.Services;

public class Argon2iCryptoHashServiceTests
{
    private const int SALT_LENGTH = 128;

    private readonly IRandomGeneratorService _rndSvc;
    private readonly ICryptoHashService _cryptoHashSvc;

    public Argon2iCryptoHashServiceTests()
    {
        _rndSvc = SecureRandomGeneratorService.Instance;
        _cryptoHashSvc = new Argon2iCryptoHashService();
    }

    [Fact]
    public async Task BinaryFullCycleFact()
    {
        var plainLen = _rndSvc.NextInt32(64, 256);
        var notPlain = _rndSvc.NextBytes(plainLen).ToArray();
        var plain = _rndSvc.NextBytes(plainLen).ToArray();
        var salt = _rndSvc.NextBytes(SALT_LENGTH).ToArray();
        var notSalt = _rndSvc.NextBytes(SALT_LENGTH).ToArray();

        var hash = await _cryptoHashSvc.HashAsync(plain, salt);

        Assert.Equal(Argon2iCryptoHashService.HASH_BYTE_COUNT, hash.Length);

        Assert.False(await _cryptoHashSvc.VerifyAsync(hash, notPlain, salt));
        Assert.False(await _cryptoHashSvc.VerifyAsync(hash, plain, notSalt));
        Assert.True(await _cryptoHashSvc.VerifyAsync(hash, plain, salt));
    }

    [Fact]
    public async Task StringFullCycleFact()
    {
        var plainLen = _rndSvc.NextInt32(64, 256);
        var plain = _rndSvc.NextString(plainLen);
        var salt = _rndSvc.NextBytes(SALT_LENGTH).ToArray();
        var base64Salt = Convert.ToBase64String(salt);

        var binaryHash = await _cryptoHashSvc.HashAsync(Encoding.UTF8.GetBytes(plain), salt);
        var base64Hash = await _cryptoHashSvc.HashAsync(plain, base64Salt);

        Assert.Equal(base64Hash, Convert.ToBase64String(binaryHash));

        Assert.True(await _cryptoHashSvc.VerifyAsync(base64Hash, plain, base64Salt));
        Assert.True(await _cryptoHashSvc.VerifyAsync(binaryHash, Encoding.UTF8.GetBytes(plain), salt));
    }
}