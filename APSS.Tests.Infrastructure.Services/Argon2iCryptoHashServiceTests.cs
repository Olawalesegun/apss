using System.Text;

using APSS.Domain.Services;
using APSS.Infrastructure.Services;

using Xunit;

namespace APSS.Tests.Infrastructure.Services;

public class Argon2iCryptoHashServiceTests
{
    #region Fields

    private const int SALT_LENGTH = 128;

    private readonly ICryptoHashService _cryptoHashSvc;
    private readonly IRandomGeneratorService _rndSvc;

    #endregion Fields

    #region Public Constructors

    public Argon2iCryptoHashServiceTests()
    {
        _rndSvc = new SecureRandomGeneratorService();
        _cryptoHashSvc = new Argon2iCryptoHashService();
    }

    #endregion Public Constructors

    #region Public Methods

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

        Assert.False(await _cryptoHashSvc.VerifyAsync(notPlain, hash, salt));
        Assert.False(await _cryptoHashSvc.VerifyAsync(plain, hash, notSalt));
        Assert.True(await _cryptoHashSvc.VerifyAsync(plain, hash, salt));
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

        Assert.True(await _cryptoHashSvc.VerifyAsync(plain, base64Hash, base64Salt));
        Assert.True(await _cryptoHashSvc.VerifyAsync(Encoding.UTF8.GetBytes(plain), binaryHash, salt));
    }

    #endregion Public Methods
}