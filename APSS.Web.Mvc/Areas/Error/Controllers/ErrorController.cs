using Microsoft.AspNetCore.Mvc;
using APSS.Web.Mvc.Areas.Error.Models;

namespace APSS.Web.Mvc.Areas.Error.Controllers;

[Area(Areas.Error)]
public class ErrorController : Controller
{
    private IActionResult Index(int status, string title, string description)
        => View(nameof(Index), new ErrorViewModel(status, title, description));

    public IActionResult _400()
        => Index(400, "Bad Request", "Oops, invalid data was detected");

    public IActionResult _401()
        => Index(401, "Unauthorized", "It seems you do not have sufficient permissions to access this resource");

    public IActionResult _404()
        => Index(404, "Not Found", "The item you look for does not exist");

    public IActionResult _409()
        => Index(409, "Conflict", "Invalid system state. Please contact the system developers.");

    public IActionResult _500()
        => Index(500, "Internal Error", "An internal server error has occured. Please try again later.");
}