using APSS.Web.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace APSS.Web.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AnimalGroup _contextAccessor;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            AnimalGroup animal = new AnimalGroup();
            animal = new AnimalGroup()
            {
                Id = 1,
                Type = "dog",
                Quantity = 100,
                Sex = "male"
            };
            return View(animal);
        }

        [HttpPost]
        public IActionResult Privacy(AnimalGroup animalGroup)
        {
            if (animalGroup != null)
                return RedirectToAction("Index", "DataManagement");
            else
            {
                return RedirectToAction("Index", "DataManagement");
            }

            return View(animalGroup);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}