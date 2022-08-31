using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using APSS.Domain.Services;
using APSS.Web.Dtos.Forms;

namespace APSS.Web.Mvc.Controllers;

public class SetupController : Controller
{
    private readonly ISetupService _setupSvc;

    public SetupController(ISetupService setupSvc)
        => _setupSvc = setupSvc;

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        if (await _setupSvc.CanSetupAsync())
            return View();

        return RedirectToAuth();
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Index([FromForm] SetupForm form)
    {
        if (!ModelState.IsValid)
            return View(form);

        var account = await _setupSvc.SetupAsync(form.HolderName, form.Password);

        return RedirectToAuth();
    }

    private IActionResult RedirectToAuth()
        => RedirectToAction(nameof(AuthController.SignIn), "Auth");
}