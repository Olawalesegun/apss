namespace APSS.Domain.Services.Exceptions;

public sealed class DisabledAccountException : Exception
{
    #region Fields

    private readonly long _accountId;

    #endregion Fields

    #region Public Constructors

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="accountId">The id of the disabled account</param>
    public DisabledAccountException(long accountId) : base($"account #{accountId} is disabled")
    {
        _accountId = accountId;
    }

    #endregion Public Constructors

    #region Properties

    /// <summary>
    /// Gets the id of the disabled account
    /// </summary>
    public long AccountId => _accountId;

    #endregion Properties
}