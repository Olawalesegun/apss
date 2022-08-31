namespace APSS.Web.Mvc.Auth;

public sealed class AuthSettings
{
    /// <summary>
    /// Gets or sets the expiration timespan of cookies
    /// </summary>
    public TimeSpan CookieExpiration { get; set; } = TimeSpan.FromMinutes(30);

    /// <summary>
    /// Gets or sets whether the app should support sliding expiration or not
    /// </summary>
    public bool SlidingExpiration { get; set; } = true;

    /// <summary>
    /// Gets or sets whether cookie should be allowed to be refreshed
    /// </summary>
    public bool AllowRefresh { get; set; }

    /// <summary>
    /// Gets or sets whether issued cookies are persistent
    /// </summary>
    public bool PersistentCookies { get; set; }
}