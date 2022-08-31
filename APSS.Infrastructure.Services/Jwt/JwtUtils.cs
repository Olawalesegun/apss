using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.IdentityModel.Tokens;

using APSS.Domain.Entities;
using APSS.Infrastructure.Services.Jwt;

namespace APSS.Infrastructure.Services;

public static class CustomClaims
{
    #region Fields

    public const string Id = "id";
    public const string Permissions = "permissions";

    #endregion Fields
}

public static class JwtUtils
{
    #region Public Methods

    /// <summary>
    /// Generates an access token
    /// </summary>
    /// <param name="account">The account to generate the token for</param>
    /// <param name="groups">The groups the user is enrolled in</param>
    /// <param name="settings">Jwt settings</param>
    /// <returns>The generated access token</returns>
    public static string GenerateAccessToekn(Account account, AuthSettings settings)
    {
        var claims = new List<Claim>()
        {
            new Claim(CustomClaims.Id, account.Id.ToString()),
            new Claim(ClaimTypes.NameIdentifier, account.User.Name),
            new Claim(ClaimTypes.Name, account.HolderName),
            new Claim(ClaimTypes.Role, account.User.AccessLevel.ToRolesString()),
            new Claim(CustomClaims.Permissions, string.Join(',', account.Permissions.GetPermissionValues())),
        };

        return GenerateToken(
            settings.AccessTokenSecret,
            settings.TokenIssuer,
            settings.TokenAudience,
            settings.AccessTokenValidity,
            claims);
    }

    /// <summary>
    /// Generates a refresh token
    /// </summary>
    /// <param name="settings">Jwt settings</param>
    /// <returns>The generated refresh token</returns>
    public static string GenerateRefereshToken(AuthSettings settings)
        => GenerateToken(
            settings.RefreshTokenSecret,
            settings.TokenIssuer,
            settings.TokenAudience,
            settings.RefreshTokenValidity);

    /// <summary>
    /// Gets principal from expired tokens
    /// </summary>
    /// <param name="token"></param>
    /// <param name="settings"></param>
    /// <returns></returns>
    /// <exception cref="SecurityTokenException"></exception>
    public static ClaimsPrincipal GetPrincipalFromExpiredToken(string token, AuthSettings settings)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = settings.TokenIssuer,
            ValidAudience = settings.TokenAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.AccessTokenSecret)),
            ValidateLifetime = false,
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(
            token, tokenValidationParameters, out SecurityToken securityToken);

        if (securityToken is not JwtSecurityToken jwtSecurityToken ||
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("invalid token");

        return principal;
    }

    /// <summary>
    /// Validates a refresh token
    /// </summary>
    /// <param name="token">The token to validate</param>
    /// <param name="settings">Jwt settings</param>
    /// <returns>True if the validation succeeds, false otherwise</returns>
    public static bool ValidateRefreshToken(string token, AuthSettings settings)
        => ValidateToken(token, settings.TokenIssuer, settings.TokenAudience, settings.RefreshTokenSecret);

    /// <summary>
    /// Validates a token
    /// </summary>
    /// <param name="token">The token to validate</param>
    /// <param name="issuer">The issuer of the token</param>
    /// <param name="audience">The audience of the token</param>
    /// <param name="key">The key to use for validation</param>
    /// <param name="validateLifeTime">If true, validate lifetime</param>
    /// <returns></returns>
    public static bool ValidateToken(
        string token,
        string issuer,
        string audience,
        string key, bool validateLifeTime = true)
    {
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = validateLifeTime,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
            ValidIssuer = issuer,
            ValidAudience = audience,
            ClockSkew = TimeSpan.Zero
        };

        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

        try
        {
            jwtSecurityTokenHandler.ValidateToken(
                token,
                validationParameters,
                out SecurityToken validatedToken);

            return true;
        }
        catch
        {
            return false;
        }
    }

    #endregion Public Methods

    #region Private Methods

    /// <summary>
    /// Generates a JWT token
    /// </summary>
    /// <param name="secretKey">The secret key used for signing</param>
    /// <param name="issuer">The issuer of the token</param>
    /// <param name="audience">The audience of the token</param>
    /// <param name="validity">The validity of the token</param>
    /// <param name="claims">Additional claims</param>
    /// <returns>The genreated token</returns>
    private static string GenerateToken(
        string secretKey,
        string issuer,
        string audience,
        TimeSpan validity,
        IEnumerable<Claim>? claims = null)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer,
            audience,
            claims,
            DateTime.UtcNow,
            DateTime.UtcNow.Add(validity),
            credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    #endregion Private Methods
}