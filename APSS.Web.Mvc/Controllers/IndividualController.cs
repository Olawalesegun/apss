using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APSS.Web.Mvc.Controllers
{
    public class IndividualController : Controller
    {
        // GET: IndividualController
        public ActionResult GetIndividuals()
        {
            return View();
        }

        // GET: IndividualController/Details/5
        public ActionResult GetIndividual(int id)
        {
            return View();
        }

        // GET: IndividualController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IndividualController/Create
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

        // GET: IndividualController/Edit/5
        public ActionResult Update(int id)
        {
            return View();
        }

        // POST: IndividualController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: IndividualController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: IndividualController/Delete/5
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