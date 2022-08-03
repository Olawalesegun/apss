using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APSS.Web.Mvc.Controllers
{
    public class FamilyIndividualController : Controller
    {
        // GET: FamilyIndividualController
        public ActionResult GetFamilyIndividual(int id)
        {
            return View();
        }

        // GET: FamilyIndividualController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FamilyIndividualController/Create
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

        // GET: FamilyIndividualController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FamilyIndividualController/Edit/5
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

        // GET: FamilyIndividualController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FamilyIndividualController/Delete/5
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