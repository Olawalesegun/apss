using APSS.Web.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace APSS.Web.Mvc.Controllers
{
    public class IndividualController : Controller
    {
        // GET: Individual/GetIndividuals
        public IActionResult GetIndividuals()
        {
            return View();
        }

        // GET: IndividualController/AddIndividual/5
        public IActionResult AddIndividual(int id)
        {
            return View();
        }

        // POST: IndividualController/AddIndividual
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddIndividual(IndividualAddDto individual)
        {
            return View(individual);
        }

        // Get: IndividualController/UpdateIndividual/5
        public IActionResult UpdateIndividual(int id)
        {
            var individual = new IndividualDto();

            return View("EditIndividual", individual);
        }

        // POST: IndividualController/UpdateIndividual/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateIndividual(int id, IndividualDto individual)
        {
            return View("EditIndividual");
        }

        // GET: IndividualController/DeleteIndividual/5
        public IActionResult DeleteIndividual(int id)
        {
            return View(nameof(GetIndividuals));
        }

        // GET: IndividualController/DeleteIndividual/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteIndividual(int id, IndividualDto individualDto)
        {
            return View(nameof(GetIndividuals));
        }
    }
}