using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;
using APSS.Domain.Services;
using APSS.Domain.Services.Exceptions;
using APSS.Web.Dtos.Forms;
using APSS.Web.Mvc.Auth;
using APSS.Web.Mvc.Filters;
using APSS.Web.Mvc.Util.Navigation.Routes;
using CustomClaims = APSS.Web.Mvc.Auth.CustomClaims;

namespace APSS.Web.Mvc.Areas.Auth.Controllers;

[Area(Areas.Auth)]
public class AuthController : Controller
{
    #region Fields

    private readonly AuthSettings _settings = new();
    private readonly IAuthService _authSvc;
    private readonly ISetupService _setupSvc;

    #endregion Fields

    #region Public Constructors

    public AuthController(IConfigurationService configSvc, IAuthService authSvc, ISetupService setupSvc)
    {
        configSvc.Bind(nameof(AuthSettings), _settings);

        _authSvc = authSvc;
        _setupSvc = setupSvc;
    }

    #endregion Public Constructors

    #region Public Methods

    [HttpGet]
    public async Task<IActionResult> Login()
    {
        if (User.Identity?.IsAuthenticated == true)
            return LocalRedirect(Routes.Dashboard.Home.FullPath);

        if (await _setupSvc.CanSetupAsync())
            return LocalRedirect(Routes.Setup.FullPath);

        return View(nameof(Login));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [TypeFilter(typeof(ExceptionHandlingFilter<InvalidAccountIdOrPasswordException>))]
    public async Task<IActionResult> Login([FromForm] SignInForm form, [FromQuery] string? returnUrl)
    {
        if (!ModelState.IsValid)
            return View(nameof(Login), form);

        var deviceInfo = HttpContext.GetLoginInfo();
        var session = await _authSvc.SignInAsync(long.Parse(form.AccountId), form.Password, deviceInfo);

        var authProperties = new AuthenticationProperties
        {
            AllowRefresh = _settings.AllowRefresh,
            ExpiresUtc = !_settings.PersistentCookies ? DateTimeOffset.UtcNow.Add(_settings.CookieExpiration) : null,
            IsPersistent = form.IsPersistent && _settings.PersistentCookies,
            IssuedUtc = DateTimeOffset.UtcNow,
            RedirectUri = returnUrl,
        };

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            AuthUtils.CreatePrincipal(session),
            authProperties);

        if (returnUrl is not null)
            return LocalRedirect($"~/{returnUrl}");

        return LocalRedirect(Routes.Dashboard.Home.FullPath);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await _authSvc.SignOutAsync(User.GetAccountId(), User.GetClaimValue(CustomClaims.Token));
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return RedirectToAction(nameof(Login));
    }

    #endregion Public Methods
}