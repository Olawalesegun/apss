using Microsoft.AspNetCore.Mvc;

namespace APSS.Web.Mvc.Controllers
{
    public class SurveyController : Controller
    {
        // GET: Survey/GetSurveys
        public ActionResult GetSurveys()
        {
            return View();
        }

        // GET: Survey/SurveyDetails/5
        public ActionResult SurveyDetails(int id)
        {
            return View();
        }

        // GET: Survey/Add Survey
        public ActionResult AddSurvey()
        {
            return View();
        }

        // POST: Survey/AddSurvey
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddSurvey(IFormCollection collection)
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

        // GET: Survey/EditSurvey/5
        public ActionResult EditSurvey(int id)
        {
            return View();
        }

        // POST: Survey/EditSurvey/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSurvey(int id, IFormCollection collection)
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

        //Get:Survey/AddQuestion/5
        public ActionResult AddQuestion(int id)
        {
            return View();
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

        //GET:Survey/confirmDeleteSurvey/5
        public IActionResult ConfirmDeleteSurvey(int id)
        {
            return View();
        }

        // GET: Survey/DeleteSurvey/5
        public ActionResult DeleteSurvey(int id)
        {
            return View();
        }

        // POST: Survey/SurveyDelete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteSurvey(int id, IFormCollection collection)
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