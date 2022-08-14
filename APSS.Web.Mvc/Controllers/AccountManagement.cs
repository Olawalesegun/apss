using Microsoft.AspNetCore.Mvc;

namespace APSS.Web.Mvc.Controllers
{
    public class AccountManagement : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        public async Task<IActionResult> EditUser()
        {
            return View();
        }

        public async Task<IActionResult> UserDetials(int id)
        {
            return View();
        }

        public async Task<IActionResult> UsersIndex()
        {
            return View();
        }
    }
}