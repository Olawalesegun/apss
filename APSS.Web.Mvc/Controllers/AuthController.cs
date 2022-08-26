using Microsoft.AspNetCore.Mvc;
using APSS.Domain.Services;
using APSS.Web.Dtos.Forms;

namespace APSS.Web.Mvc.Controllers;

public class AuthController : Controller
{
    #region Fields

    private readonly IAuthService _authSvc;

    #endregion Fields

    #region Public Constructors

    public AuthController(IAuthService authSvc)
    {
        _authSvc = authSvc;
    }

    #endregion Public Constructors

    #region Public Methods

    [HttpGet]
    public IActionResult SignIn()
        => View("SignIn", new SignInForm());

    [HttpPost("SignIn")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SignIn([FromForm] SignInForm form)
    {
        if (!ModelState.IsValid)
            return View(form);

        //var (account, token) = await _authSvc.SignInAsync(long.Parse(form.AccountId), form.Password, null!);
        return View(form);
    }

    #endregion Public Methods
}