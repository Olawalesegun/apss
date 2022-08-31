namespace APSS.Domain.Services.Exceptions;

public sealed class ExpiredSessionException : Exception
{
    #region Fields

    private readonly long _accountId;
    private readonly long _sessionId;

    #endregion Fields

    #region Public Constructors

    /// <summary>
    /// Default constructuro
    /// </summary>
    /// <param name="accountId">The id of the account refreshing the session</param>
    /// <param name="sessionId">The id of the session being refreshed</param>
    public ExpiredSessionException(long accountId, long sessionId)
        : base($"account #{accountId} tried to refresh an expired session #{sessionId}")
    {
        _accountId = accountId;
        _sessionId = sessionId;
    }

    #endregion Public Constructors

    #region Properties

    /// <summary>
    /// Gets the id of the account trying to refresh the session
    /// </summary>
    public long AccountId => _accountId;

    /// <summary>
    /// Gets the id of the session being refreshed
    /// </summary>
    public long SessionId => _sessionId;

    #endregion Properties
}