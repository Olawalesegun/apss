using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using APSS.Domain.Services;
using APSS.Domain.Services.Exceptions;
using APSS.Web.Dtos.Forms;
using APSS.Web.Mvc.Auth;
using APSS.Web.Mvc.Filters;
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
    public IActionResult SignIn()
    {
        if (User.Identity is not null)
            return View();

        return View(nameof(SignIn));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [TypeFilter(typeof(ExceptionHandlingFilter<InvalidAccountIdOrPasswordException>))]
    public async Task<IActionResult> SignIn([FromForm] SignInForm form, [FromQuery] string? returnUrl)
    {
        if (!ModelState.IsValid)
            return View(nameof(SignIn), form);

        if (User.Identity is not null)
            return RedirectToAction("Index", "AnimalGroup");

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
            return LocalRedirect(returnUrl);

        return RedirectToAction("Index", "AnimalGroup");
    }

    [HttpGet("SignOut")]
    [Authorize]
    public async Task<IActionResult> DoSignOut()
    {
        await _authSvc.SignOutAsync(User.GetId(), User.GetClaimValue(CustomClaims.Token));
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return RedirectToAction(nameof(SignIn));
    }

    #endregion Public Methods
}