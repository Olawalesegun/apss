using Microsoft.AspNetCore.Mvc;

namespace APSS.Web.Mvc.Areas.Home.Controllers
{
    [Area(Areas.Home)]
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}