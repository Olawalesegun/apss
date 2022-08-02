using Microsoft.AspNetCore.Mvc;

namespace APSS.Web.Mvc.Controllers
{
    public class LandController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
