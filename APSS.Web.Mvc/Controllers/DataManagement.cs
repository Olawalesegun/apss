using Microsoft.AspNetCore.Mvc;

namespace APSS.Web.Mvc.Controllers
{
    public class DataManagement : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
