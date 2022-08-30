using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using APSS.Domain.Services;

namespace APSS.Web.Mvc.Auth;

public class TokenValidationEvent : CookieAuthenticationEvents
{
    private readonly IAuthService _authSvc;

    public TokenValidationEvent(IAuthService authSvc)
    {
        _authSvc = authSvc;
    }

    public override async Task ValidatePrincipal(CookieValidatePrincipalContext context)
    {
        try
        {
            var accountPrincipal = context.Principal!;

            var id = long.Parse(accountPrincipal.Claims.First(c => c.Type == CustomClaims.Id).Value);
            var token = accountPrincipal.Claims.First(c => c.Type == CustomClaims.Token).Value;

            if (string.IsNullOrEmpty(token) || !await _authSvc.IsTokenValidAsync(id, token, ""))
                throw new Exception(); // to be changed!
        }
        catch (Exception)
        {
            context.RejectPrincipal();

            await context.HttpContext.SignOutAsync(
                  CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}