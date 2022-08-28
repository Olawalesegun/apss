using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using APSS.Domain.Entities;
using APSS.Domain.Services;
using APSS.Infrastructure.Services;
using APSS.Web.Dtos.Forms;
using APSS.Web.Mvc.Auth;
using CustomClaims = APSS.Web.Mvc.Auth.CustomClaims;

namespace APSS.Web.Mvc.Controllers;

public class AuthController : Controller
{
    #region Fields

    private readonly AuthSettings _settings = new();
    private readonly IAuthService _authSvc;

    #endregion Fields

    #region Public Constructors

    public AuthController(IConfigurationService configSvc, IAuthService authSvc)
    {
        configSvc.Bind(nameof(AuthSettings), _settings);

        _authSvc = authSvc;
    }

    #endregion Public Constructors

    #region Public Methods

    [HttpGet]
    public IActionResult SignIn() => View(nameof(SignIn));

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SignIn([FromForm] SignInForm form, [FromQuery] string? returnUrl)
    {
        if (!ModelState.IsValid)
            return View(nameof(SignIn), form);

        var deviceInfo = new LoginInfo
        {
            HostName = HttpContext.Request.Host.Value,
            UserAgent = HttpContext.Request.Headers.UserAgent,
            LastIpAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown"
        };

        var (account, token) = await _authSvc.SignInAsync(long.Parse(form.AccountId), form.Password, deviceInfo);

        var claims = new List<Claim>()
        {
            new Claim(CustomClaims.Id, account.Id.ToString(), ClaimValueTypes.Integer64),
            new Claim(ClaimTypes.NameIdentifier, account.User.Name),
            new Claim(ClaimTypes.Name, account.HolderName),
            new Claim(ClaimTypes.Role, account.User.AccessLevel.ToRolesString()),
            new Claim(CustomClaims.Token, token.Value),
            new Claim(CustomClaims.Permissions, string.Join(',', account.Permissions.GetPermissionValues())),
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties
        {
            AllowRefresh = _settings.AllowRefresh,
            ExpiresUtc = !_settings.PersistentCookies ? DateTimeOffset.UtcNow.Add(_settings.CookieExpiration) : null,
            IsPersistent = _settings.PersistentCookies,
            IssuedUtc = DateTimeOffset.UtcNow,
            RedirectUri = returnUrl,
        };

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);

        return LocalRedirect("/");
    }

    #endregion Public Methods
}