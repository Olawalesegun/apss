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
            var principal = context.Principal!;

            var accountId = principal.GetId();
            var token = principal.GetClaimValue(CustomClaims.Token);
            var status = await _authSvc.ValidateTokenAsync(accountId, token);

            if (status == TokenValidationResult.Valid)
            {
                return;
            }
            else if (status == TokenValidationResult.NeedsRefreshing || context.ShouldRenew)
            {
                var session = await _authSvc.RefreshAsync(
                    accountId,
                    token,
                    context.HttpContext.GetLoginInfo());

                context.ReplacePrincipal(AuthUtils.CreatePrincipal(session));
                return;
            }
        }
        catch (Exception)
        {
        }

        context.RejectPrincipal();

        await context.HttpContext.SignOutAsync(
              CookieAuthenticationDefaults.AuthenticationScheme);
    }
}