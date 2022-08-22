using Microsoft.IdentityModel.Tokens;

using APSS.Domain.Entities;
using APSS.Domain.Repositories;
using APSS.Domain.Repositories.Extensions;
using APSS.Domain.Services;
using APSS.Domain.Services.Exceptions;
using APSS.Infrastructure.Services.Jwt;

namespace APSS.Infrastructure.Services;

public sealed class JwtAuthService : IAuthService
{
    #region Fields

    private readonly ICryptoHashService _cryptoHashSvc;
    private readonly IRandomGeneratorService _rndSvc;
    private readonly IUnitOfWork _uow;
    private readonly AuthSettings _settings = new();

    #endregion Fields

    #region Public Constructors

    public JwtAuthService(
        IConfigurationService configSvc,
        IRandomGeneratorService rndSvc,
        ICryptoHashService cryptoHashSvc,
        IUnitOfWork uow)
    {
        configSvc.Bind(nameof(AuthSettings), _settings);

        _rndSvc = rndSvc;
        _cryptoHashSvc = cryptoHashSvc;
        _uow = uow;
    }

    #endregion Public Constructors

    #region Public Methods

    /// <inheritdoc/>
    public async Task<(Account, RefreshToken)> SignInAsync(
        long accountId,
        string password,
        LoginDeviceInfo deviceInfo)
    {
        var account = await _uow.Accounts.Query().FindOrNullAsync(accountId);

        if (account is null || !await _cryptoHashSvc.VerifyAsync(password, account.PasswordHash, account.PasswordSalt))
            throw new InvalidAccountIdOrPasswordException();

        if (await _uow.RefreshTokens.Query().FirstOrNullAsync(
            r => r.Owner.Id == account.Id &&
            r.UniqueIdentifier == deviceInfo.UniqueIdentifier &&
            r.ValidUntil > DateTime.Now)
            is var token && token is not null)
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
            Token = JwtUtils.GenerateRefereshToken(_settings),
            UniqueIdentifier = deviceInfo.UniqueIdentifier,
            LastIpAddress = deviceInfo.LastIpAddress,
            LastLogin = DateTime.Now,
            HostName = deviceInfo.HostName,
            DeviceName = deviceInfo.DeviceName,
            ValidUntil = DateTime.Now.Add(_settings.RefreshTokenValidity),
        };

        _uow.RefreshTokens.Add(refreshToken);
        await _uow.CommitAsync();

        return (account, refreshToken);
    }

    /// <inheritdoc/>
    public async Task<bool> IsTokenValidAsync(long accountId, string token, string uniqueId)
        => await _uow.RefreshTokens.Query().AnyAsync(
            r => r.Owner.Id == accountId &&
            r.Token == token &&
            r.UniqueIdentifier == uniqueId &&
            r.ValidUntil > DateTime.Now);

    /// <inheritdoc/>
    public async Task SignOutAsync(long accountId, string token, string uniqueId)
    {
        var refreshToken = await _uow.RefreshTokens.Query().FirstAsync(r =>
            r.Owner.Id == accountId &&
            r.Token == token &&
            r.UniqueIdentifier == uniqueId);

        _uow.RefreshTokens.Remove(refreshToken);
        await _uow.CommitAsync();
    }

    /// <inheritdoc/>
    public async Task<string> RefreshAsync(string refreshToken, string accessToken, string uniqueIdentifier)
    {
        if (!JwtUtils.ValidateRefreshToken(refreshToken, _settings) ||
            !JwtUtils.ValidateToken(accessToken,
                                    _settings.TokenIssuer,
                                    _settings.TokenAudience,
                                    _settings.AccessTokenSecret,
                                    false))
        {
            throw new SecurityTokenException("invalid refresh and/or access token");
        }

        var accessPrincipal = JwtUtils.GetPrincipalFromExpiredToken(accessToken, _settings);
        var user = await _uow.Accounts.Query().FindAsync(accessPrincipal.GetId());

        if (!await IsTokenValidAsync(user.Id, refreshToken, uniqueIdentifier))
            throw new SecurityTokenException("invalid refresh token");

        return JwtUtils.GenerateAccessToekn(user, _settings);
    }

    #endregion Public Methods
}