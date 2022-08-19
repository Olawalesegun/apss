using APSS.Domain.Services;
using APSS.Web.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APSS.Web.Mvc.Controllers
{
    public class PopulationController : Controller
    {
        private readonly IPopulationService _populationSvc;

        public PopulationController(IPopulationService populationSvc)
        {
            _populationSvc = populationSvc;
        }

        public ActionResult Index()
        {
            return View();
        }

        // GET: FamilyController
        public ActionResult GetFamilies()
        {
            long id;
            /*            List<FamilyDto> families = await _populationSvc.GetFamiliesAsync(id);
            */
            return View();
        }

        public ActionResult FamilyDetails(int id)
        {
            return View();
        }

        // GET: FamilyController/Details/5
        public ActionResult GetIndividuals()
        {
            return View();
        }

        public ActionResult IndividualDetails(int id)
        {
            return View();
        }

        // GET: FamilyController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FamilyController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FamilyController/Edit/5
        public ActionResult Update(int id)
        {
            return View();
        }

        // POST: FamilyController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FamilyController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FamilyController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}