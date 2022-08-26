namespace APSS.Domain.Entities;

/// <summary>
/// A class to represent a user refresh token
/// </summary>
public sealed class RefreshToken : AuditableEntity
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
    /// Gets or sets the hostname of the device that uses the token
    /// </summary>
    public string? HostName { get; set; }

    /// <summary>
    /// Gets or sets the user agent of the user's browser
    /// </summary>
    public string? Agent { get; set; }

    /// <summary>
    /// Gets or sets the token string
    /// </summary>
    public string Value { get; set; } = null!;

    /// <summary>
    /// Gets or sets the expiration date of the token
    /// </summary>
    public DateTime ValidUntil { get; set; }

    /// <summary>
    /// Gets or sets the owner account
    /// </summary>
    public Account Owner { get; set; } = null!;
}