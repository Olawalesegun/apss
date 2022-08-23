using Microsoft.AspNetCore.Mvc;
using APSS.Web.Dtos.Forms;

namespace APSS.Web.Mvc.Controllers;

public class AuthController : Controller
{
    [HttpGet]
    public IActionResult SignIn()
    {
        return View("SignIn", new SignInForm());
    }

    [HttpPost("SignIn")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SignIn([FromForm] SignInForm form)
    {
        return View(form);
    }
}