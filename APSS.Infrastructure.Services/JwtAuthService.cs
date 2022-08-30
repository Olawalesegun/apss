using APSS.Domain.Entities;
using APSS.Domain.Repositories;
using APSS.Domain.Repositories.Extensions;
using APSS.Domain.Services;
using APSS.Domain.Services.Exceptions;

namespace APSS.Infrastructure.Services;

public sealed class JwtAuthService : IAuthService
{
    #region Fields

    private readonly TokenSettings _settings = new();

    private readonly ICryptoHashService _cryptoHashSvc;
    private readonly IRandomGeneratorService _rndSvc;
    private readonly IUnitOfWork _uow;

    #endregion Fields

    #region Public Constructors

    public JwtAuthService(
        IConfigurationService configSvc,
        IRandomGeneratorService rndSvc,
        ICryptoHashService cryptoHashSvc,
        IUnitOfWork uow)
    {
        configSvc.Bind("TokenSettings", _settings);

        _rndSvc = rndSvc;
        _cryptoHashSvc = cryptoHashSvc;
        _uow = uow;
    }

    #endregion Public Constructors

    #region Public Methods

    /// <inheritdoc/>
    public async Task<(Account, RefreshToken)> SignInAsync(long accountId, string password, LoginInfo info)
    {
        var account = await _uow.Accounts.Query().FindOrNullAsync(accountId);

        if (account is null || !await _cryptoHashSvc.VerifyAsync(password, account.PasswordHash, account.PasswordSalt))
            throw new InvalidAccountIdOrPasswordException();

        if (await _uow.RefreshTokens.Query()
                  .FirstOrNullAsync(r => r.Owner.Id == account.Id &&
                                         r.ValidUntil > DateTime.Now) is var token && token is not null)
        {
            token.LastLogin = DateTime.Now;

            _uow.RefreshTokens.Update(token);
            await _uow.CommitAsync();

            return (account, token);
        }

        if (await _uow.RefreshTokens.Query().CountAsync(
                  r => r.Owner.Id == account.Id &&
                       r.ValidUntil > DateTime.Now) >= _settings.MaxSessionsCount)
        {
            throw new MaxSessionsCountExceeded(_settings.MaxSessionsCount);
        }

        var refreshToken = new RefreshToken
        {
            Owner = account,
            Value = _rndSvc.NextString(_settings.TokenLength),
            LastIpAddress = info.LastIpAddress,
            LastLogin = DateTime.Now,
            HostName = info.UserAgent,
            Agent = info.UserAgent,
            ValidUntil = DateTime.Now.Add(_settings.ExpireTimeSpan),
        };

        _uow.RefreshTokens.Add(refreshToken);
        await _uow.CommitAsync();

        return (account, refreshToken);
    }

    /// <inheritdoc/>
    public async Task<bool> IsTokenValidAsync(long accountId, string token)
        => await _uow.RefreshTokens.Query().AnyAsync(
            r => r.Owner.Id == accountId &&
            r.Value == token &&
            r.ValidUntil > DateTime.Now);

    /// <inheritdoc/>
    public async Task SignOutAsync(long accountId, string token)
    {
        var refreshToken = await _uow.RefreshTokens.Query().FirstAsync(r =>
            r.Owner.Id == accountId &&
            r.Value == token);

        _uow.RefreshTokens.Remove(refreshToken);
        await _uow.CommitAsync();
    }

    /// <inheritdoc/>
    public async Task<string> RefreshAsync(string refreshToken, string accessToken, string uniqueIdentifier)
    {
        throw new NotImplementedException();
    }
}

public sealed class TokenSettings
{
    /// <summary>
    /// Gets or sets the length of generated tokens
    /// </summary>
    public int TokenLength { get; set; }

    /// <summary>
    /// Gets or sets the maximum sessions count
    /// </summary>
    public int MaxSessionsCount { get; set; }

    /// <summary>
    /// Gets or sets the expiration time span of the token
    /// </summary>
    public TimeSpan ExpireTimeSpan { get; set; }
}