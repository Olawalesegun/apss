using System.Text.Encodings.Web;

namespace APSS.Domain.Services.Exceptions;

public sealed class InvalidSessionException : Exception
{
    #region Fields

    private readonly long _accountId;
    private readonly string _token;

    #endregion Fields

    #region Public Constructors

    /// <summary>
    /// Default constructuro
    /// </summary>
    /// <param name="accountId">The id of the account trying to use the invalid token</param>
    /// <param name="token">The token value</param>
    public InvalidSessionException(long accountId, string token)
        : base($"account #{accountId} tried to sign in with an invalid token `{HtmlEncoder.Default.Encode(token)}")
    {
        _accountId = accountId;
        _token = token;
    }

    #endregion Public Constructors

    #region Properties

    /// <summary>
    /// Gets the id of the account trying to use the invalid token
    /// </summary>
    public long AccountId => _accountId;

    /// <summary>
    /// Gets the token value
    /// </summary>
    public string Token => _token;

    #endregion Properties
}