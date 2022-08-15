using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APSS.Web.Mvc.Controllers
{
    public class SurveysManagement : Controller
    {
        // GET: ServeyManagement
        public ActionResult GetSurveys()
        {
            return View();
        }

        public ActionResult GetSurveysEntry()
        {
            return View();
        }

        public ActionResult AddQuestion()
        {
            return View();
        }

        // GET: ServeyManagement/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ServeyManagement/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult SurveyDetails(int id)
        {
            return View();
        }

        public ActionResult SurveyEntryDetails(int id)
        {
            return View();
        }

        // POST: ServeyManagement/Create
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

        // GET: ServeyManagement/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ServeyManagement/Edit/5
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddQuestion(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(AddQuestion));
            }
            catch
            {
                return View();
            }
        }

        // GET: ServeyManagement/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ServeyManagement/Delete/5
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