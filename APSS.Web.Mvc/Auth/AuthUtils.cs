using System.Security.Claims;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

using APSS.Domain.Entities;
using APSS.Domain.Entities.Util;
using APSS.Domain.Services;

namespace APSS.Web.Mvc.Auth;

public static class AuthUtils
{
    public static ClaimsPrincipal CreatePrincipal(Session session)
    {
        var account = session.Owner;

        var claims = new List<Claim>()
        {
            new Claim(CustomClaims.Id, account.Id.ToString(), ClaimValueTypes.Integer64),
            new Claim(ClaimTypes.NameIdentifier, account.User.Name),
            new Claim(ClaimTypes.Name, account.HolderName),
            new Claim(ClaimTypes.Role, account.User.AccessLevel.ToString()),
            new Claim(CustomClaims.Token, session.Token),
            new Claim(CustomClaims.Permissions, string.Join(',', account.Permissions.GetSetNames())),
        };

        return new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));
    }

    public static LoginInfo GetLoginInfo(this HttpContext self)
    {
        return new LoginInfo
        {
            UserAgent = self.Connection.RemoteIpAddress!.ToString(),
            IpAddress = self.Connection.RemoteIpAddress?.ToString() ?? "unknown"
        };
    }
}