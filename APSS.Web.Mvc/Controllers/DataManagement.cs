using APSS.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace APSS.Web.Mvc.Controllers
{
    public class DataManagement : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AnimalProduct()
        {
            return View();
        }

        public async Task<IActionResult> CreateAnimalProduct()
        {
            return View();
        }

        public async Task<IActionResult> AnimalDetails()
        {
            return View();
        }

        public async Task<IActionResult> AnimalProductdetails()
        {
            return View();
        }
    }
}