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
    Task<(Account, RefreshToken)> SignInAsync(long accountId, string password, LoginInfo deviceInfo);

    /// <summary>
    /// Asynchronously checks if a token is valid or not
    /// </summary>
    /// <param name="accountId">The id of the owner of the token</param>
    /// <param name="token">The token to check</param>
    /// <param name="uniqueId">The device unique id</param>
    /// <returns></returns>
    Task<bool> IsTokenValidAsync(long accountId, string token, string uniqueId);

    /// <summary>
    /// Asynchronously invlidates a refresh token
    /// </summary>
    /// <param name="accountId">The id of the owner of the token</param>
    /// <param name="token">The token to check</param>
    /// <param name="uniqueId">The device unique id</param>
    /// <returns></returns>
    Task SignOutAsync(long accountId, string token, string uniqueId);

    /// <summary>
    /// Asynchronously generates an access token with a refresh token and an expired access toekn
    /// </summary>
    /// <param name="refreshToken">the refresh token to generate with</param>
    /// <param name="accessToken">The access token to generate with</param>
    /// <param name="uniqueIdentifier">The device unique id</param>
    /// <returns>The generated access token</returns>
    Task<string> RefreshAsync(string refreshToken, string accessToken, string uniqueIdentifier);
}

public sealed class LoginInfo
{
    /// <summary>
    /// Gets or sets the last ip address that used the token
    /// </summary>
    public string LastIpAddress { get; set; } = null!;

    /// <summary>
    /// Gets or sets the device name of the device that uses the token
    /// </summary>
    public string? HostName { get; set; }

    /// <summary>
    /// Gets or sets the hostname of the device that uses the token
    /// </summary>
    public string? UserAgent { get; set; }
}