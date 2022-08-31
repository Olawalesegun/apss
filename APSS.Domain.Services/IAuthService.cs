using APSS.Domain.Entities;

namespace APSS.Domain.Services;

public interface IAuthService
{
    /// <summary>
    /// Asynchronously logs a user in to an account
    /// </summary>
    /// <param name="accountId">The id of the account to sign in with</param>
    /// <param name="password">The password of the account</param>
    /// <param name="deviceInfo">The info of the device that requested the token</param>
    /// <returns>The created refresh token</returns>
    /// <exception cref="InvalidUsernameOrPasswordException"></exception>
    /// <exception cref="MaxSessionsCountExceeded"></exception>
    Task<Session> SignInAsync(long accountId, string password, LoginInfo deviceInfo);

    /// <summary>
    /// Asynchronously validates a token
    /// </summary>
    /// <param name="accountId">The id of the owner of the token</param>
    /// <param name="token">The token to check</param>
    /// <returns></returns>
    Task<TokenValidationResult> ValidateTokenAsync(long accountId, string token);

    /// <summary>
    /// Asynchronously invlidates a refresh token
    /// </summary>
    /// <param name="accountId">The id of the owner of the token</param>
    /// <param name="token">The token to check</param>
    /// <returns></returns>
    Task SignOutAsync(long accountId, string token);

    /// <summary>
    /// Asynchronously refreshes a token
    /// </summary>
    /// <param name="accountId">The id of the account to refresh the session for</param>
    /// <param name="token">The token to refresh</param>
    /// <param name="deviceInfo">The info of the device that requested the token</param>
    /// <returns>The generated access token</returns>
    Task<Session> RefreshAsync(long accountId, string token, LoginInfo info);
}

public sealed class LoginInfo
{
    /// <summary>
    /// Gets or sets the last ip address that used the token
    /// </summary>
    public string IpAddress { get; set; } = null!;

    /// <summary>
    /// Gets or sets the hostname of the device that uses the token
    /// </summary>
    public string UserAgent { get; set; } = null!;
}

public enum TokenValidationResult
{
    Invalid,
    NeedsRefreshing,
    Expired,
    Valid,
}