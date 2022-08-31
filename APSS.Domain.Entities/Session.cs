namespace APSS.Domain.Entities;

/// <summary>
/// A class to represent a user session
/// </summary>
public sealed class Session : AuditableEntity
{
    /// <summary>
    /// Gets or sets the last ip address that used the token
    /// </summary>
    public string LastIpAddress { get; set; } = null!;

    /// <summary>
    /// Gets or sets the date and time of the last login
    /// </summary>
    public DateTime LastLogin { get; set; }

    /// <summary>
    /// Gets or sets the user agent of the user's browser
    /// </summary>
    public string Agent { get; set; } = null!;

    /// <summary>
    /// Gets or sets the token string
    /// </summary>
    public string Token { get; set; } = null!;

    /// <summary>
    /// Gets or sets the expiration date of the token
    /// </summary>
    public DateTime ValidUntil { get; set; }

    /// <summary>
    /// Gets or sets the owner account
    /// </summary>
    public Account Owner { get; set; } = null!;
}