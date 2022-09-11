using System.Text;

using APSS.Domain.Entities;
using APSS.Domain.Repositories;
using APSS.Domain.Repositories.Extensions;
using APSS.Domain.Services;
using APSS.Domain.Services.Exceptions;

namespace APSS.Application.App;

public sealed class AuthService : IAuthService
{
    #region Fields

    private readonly ICryptoHashService _cryptoHashSvc;
    private readonly IRandomGeneratorService _rndSvc;
    private readonly TokenSettings _settings = new();
    private readonly IUnitOfWork _uow;

    #endregion Fields

    #region Public Constructors

    public AuthService(
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
    public async Task<Session> RefreshAsync(long accountId, string token, LoginInfo info)
    {
        var session = await _uow.Sessions.Query()
            .Include(s => s.Owner)
            .Include(s => s.Owner.User)
            .FirstOrNullAsync(s => s.Owner.Id == accountId && s.Token == token);

        if (session is null)
            throw new InvalidSessionException(accountId, token);

        if (session.ValidUntil < DateTime.Now)
            throw new ExpiredSessionException(accountId, session.Id);

        if (!session.Owner.IsActive)
            throw new DisabledAccountException(accountId);

        session = UpdateSessionWith(accountId, session, info);

        _uow.Sessions.Update(session);
        await _uow.CommitAsync();

        return session;
    }

    /// <inheritdoc/>
    public async Task<Session> SignInAsync(long accountId, string password, LoginInfo info)
    {
        var account = await _uow.Accounts.Query()
            .Include(s => s.User)
            .FindOrNullAsync(accountId);

        if (account is null || !await _cryptoHashSvc.VerifyAsync(password, account.PasswordHash, account.PasswordSalt))
            throw new InvalidAccountIdOrPasswordException();

        if (!account.IsActive)
            throw new DisabledAccountException(accountId);

        var validSessions = await ValidSessionsOf(accountId)
            .OrderBy(s => s.CreatedAt)
            .AsAsyncEnumerable()
            .ToListAsync();

        Session session;

        if (validSessions.Count >= _settings.MaxSessionsCount)
        {
            session = UpdateSessionWith(accountId, validSessions.First(), info);

            _uow.Sessions.Update(session);
        }
        else
        {
            session = UpdateSessionWith(accountId, new()
            {
                Owner = account,
                ValidUntil = DateTime.Now.Add(_settings.ExpiresAfter),
            }, info);

            _uow.Sessions.Add(session);
        }

        await _uow.CommitAsync();

        return session;
    }

    /// <inheritdoc/>
    public async Task SignOutAsync(long accountId, string token)
    {
        var session = await ValidSessionsOf(accountId).FirstAsync(r => r.Token == token);

        _uow.Sessions.Remove(session);
        await _uow.CommitAsync();
    }

    /// <inheritdoc/>
    public async Task<TokenValidationResult> ValidateTokenAsync(long accountId, string token)
    {
        var session = await _uow.Sessions.Query()
            .FirstOrNullAsync(s => s.Owner.Id == accountId && s.Token == token);

        if (session is null)
            return TokenValidationResult.Invalid;

        if (session.ValidUntil < DateTime.Now)
            return TokenValidationResult.Expired;

        if (session.LastLogin > DateTime.Now.Add(_settings.NeedsRefreshEvery))
            return TokenValidationResult.NeedsRefreshing;

        return TokenValidationResult.Valid;
    }

    #endregion Public Methods

    #region Private Methods

    private string GenerateToken(long accountId, LoginInfo info)
    {
        var entropy = _rndSvc
            .NextBytes(_settings.TokenEntropyLength)
            .Concat(Encoding.UTF8.GetBytes(accountId.ToString()))
            .Concat(Encoding.UTF8.GetBytes(info.UserAgent))
            .Concat(Encoding.UTF8.GetBytes(info.IpAddress));

        return Convert.ToBase64String(entropy.ToArray());
    }

    private Session UpdateSessionWith(long accountId, Session existing, LoginInfo info)
    {
        existing.LastIpAddress = info.IpAddress;
        existing.LastLogin = DateTime.Now;
        existing.Agent = info.UserAgent;
        existing.Token = GenerateToken(accountId, info);

        return existing;
    }

    private IQueryBuilder<Session> ValidSessionsOf(long accountId)
    {
        return _uow.Sessions
            .Query()
            .Include(s => s.Owner)
            .Include(s => s.Owner.User)
            .Where(s => s.Owner.Id == accountId && s.ValidUntil > DateTime.Now);
    }

    #endregion Private Methods
}

public sealed class TokenSettings
{
    #region Properties

    /// <summary>
    /// Gets or sets the expiration time span of the token
    /// </summary>
    public TimeSpan ExpiresAfter { get; set; }

    /// <summary>
    /// Gets or sets the maximum sessions count
    /// </summary>
    public int MaxSessionsCount { get; set; }

    /// <summary>
    /// Gets or sets the timespan in which the token needs to be refreshed
    /// </summary>
    public TimeSpan NeedsRefreshEvery { get; set; }

    /// <summary>
    /// Gets or sets the length of generated tokens
    /// </summary>
    public int TokenEntropyLength { get; set; }

    #endregion Properties
}