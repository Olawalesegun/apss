namespace APSS.Infrastructure.Services.Jwt;

public class AuthSettings
{
    #region Properties

    /// <summary>
    /// Gets or sets the access token secret string
    /// </summary>
    public string AccessTokenSecret { get; set; } = null!;

    /// <summary>
    /// Gets or sets the access token token validity
    /// </summary>
    public TimeSpan AccessTokenValidity { get; set; } = TimeSpan.FromMinutes(5);

    /// <summary>
    /// Gets or sets the maximum sessions count per user
    /// </summary>
    public int MaxSessionsCount { get; set; } = 4;

    /// <summary>
    /// Gets or sets the refresh token secret string
    /// </summary>
    public string RefreshTokenSecret { get; set; } = null!;

    /// <summary>
    /// Gets or sets the referesh token validity
    /// </summary>
    public TimeSpan RefreshTokenValidity { get; set; } = TimeSpan.FromDays(120);

    /// <summary>
    /// Gets or sets the token's audience
    /// </summary>
    public string TokenAudience { get; set; } = null!;

    /// <summary>
    /// Gets or sets the token's issuer
    /// </summary>
    public string TokenIssuer { get; set; } = null!;

    #endregion Properties
}