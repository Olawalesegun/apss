using System.Security.Claims;

using Microsoft.IdentityModel.Tokens;

namespace APSS.Infrastructure.Services.Jwt;

/// <summary>
/// Utility extensions to the <see cref="ClaimsPrincipal"/> class
/// </summary>
public static class ClaimsPrincipalExtensions
{
    #region Public Methods

    /// <summary>
    /// Gets the holder name from a claims principal
    /// </summary>
    public static string GetHolderName(this ClaimsPrincipal self)
        => GetClaimValue(self, ClaimTypes.Name);

    /// <summary>
    /// Gets the id from a claims principal
    /// </summary>
    public static long GetId(this ClaimsPrincipal self)
        => Convert.ToInt64(GetClaimValue(self, CustomClaims.Id));

    /// <summary>
    /// Gets the name from a claims principal
    /// </summary>
    public static string GetName(this ClaimsPrincipal self)
        => GetClaimValue(self, ClaimTypes.NameIdentifier);

    /// <summary>
    /// Gets the roles from a claims principal
    /// </summary>
    public static IEnumerable<string> Permissions(this ClaimsPrincipal self)
        => GetClaimValue(self, CustomClaims.Permissions).Split(',');

    /// <summary>
    /// Gets the roles from a claims principal
    /// </summary>
    public static IEnumerable<string> Roles(this ClaimsPrincipal self)
        => GetClaimValue(self, ClaimTypes.Role).Split(',');

    #endregion Public Methods

    #region Private Methods

    /// <summary>
    /// Gets a claim value from a claims principal
    /// </summary>
    /// <param name="claims"></param>
    /// <param name="claimKey"></param>
    /// <returns></returns>
    /// <exception cref="SecurityTokenException"></exception>
    private static string GetClaimValue(ClaimsPrincipal claims, string claimKey)
    {
        var value = claims.Claims.FirstOrDefault(c => c.Type == claimKey)?.Value;

        if (value == null)
            throw new SecurityTokenException("invalid token");

        return value;
    }

    #endregion Private Methods
}