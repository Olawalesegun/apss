using Microsoft.AspNetCore.Mvc;

namespace APSS.Web.Mvc.Controllers
{
    public class LandProductExpenseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DeleteProductExpense()
        {
            return View();
        }
    }
}