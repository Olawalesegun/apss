using Microsoft.AspNetCore.Mvc;

namespace APSS.Web.Mvc.Controllers
{
    public class AccountManagement : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
