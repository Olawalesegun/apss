using System.Security.Claims;

using Microsoft.IdentityModel.Tokens;

using APSS.Domain.Entities;

namespace APSS.Web.Mvc.Auth;

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
    /// Gets the permissions from a claims principal
    /// </summary>
    public static PermissionType GetPermissions(this ClaimsPrincipal self)
    {
        return GetClaimValue(self, CustomClaims.Permissions).Split(',')
            .Select(p => Enum.Parse<PermissionType>(p))
            .Aggregate((lhs, rhs) => lhs | rhs);
    }

    /// <summary>
    /// Gets the access level from a claims principal
    /// </summary>
    public static AccessLevel GetAccessLevel(this ClaimsPrincipal self)
        => Enum.Parse<AccessLevel>(self.GetClaimValue(ClaimTypes.Role));

    /// <summary>
    /// Gets whether a user is in an access level or not
    /// </summary>
    /// <param name="self"></param>
    /// <param name="level"></param>
    /// <returns></returns>
    public static bool IsInLevel(this ClaimsPrincipal self, AccessLevel level)
        => self.IsInRole(Enum.GetName(level)!);

    #endregion Public Methods

    #region Private Methods

    /// <summary>
    /// Gets a claim value from a claims principal
    /// </summary>
    /// <param name="claims"></param>
    /// <param name="claimKey"></param>
    /// <returns></returns>
    /// <exception cref="SecurityTokenException"></exception>
    public static string GetClaimValue(this ClaimsPrincipal claims, string claimKey)
    {
        var value = claims.Claims.FirstOrDefault(c => c.Type == claimKey)?.Value;

        if (value == null)
            throw new SecurityTokenException($"principal does not have claim `{claimKey}`");

        return value;
    }

    #endregion Private Methods
}