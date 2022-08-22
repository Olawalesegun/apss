using APSS.Web.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace APSS.Web.Mvc.Controllers
{
    public class IndividualController : Controller
    {
        // GET: Individual/GetIndividuals
        public ActionResult GetIndividuals()
        {
            return View();
        }

        // GET: IndividualController/AddIndividual/5
        public ActionResult AddIndividual(int id)
        {
            return View();
        }

        // POST: IndividualController/AddIndividual
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddIndividual(IndividualAddDto individual)
        {
            return View(individual);
        }

        // Get: IndividualController/UpdateIndividual/5
        public ActionResult UpdateIndividual(int id)
        {
            return View("EditIndividual");
        }

        // POST: IndividualController/UpdateIndividual/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateIndividual(int id, IndividualDto individual)
        {
            return View("EditIndividual");
        }

        // GET: IndividualController/DeleteIndividual/5
        public ActionResult DeleteIndividual(int id)
        {
            return View(nameof(GetIndividuals));
        }

        // GET: IndividualController/DeleteIndividual/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteIndividual(int id, IndividualDto individualDto)
        {
            return View(nameof(GetIndividuals));
        }
    }
}