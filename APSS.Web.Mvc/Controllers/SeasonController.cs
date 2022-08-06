using Microsoft.AspNetCore.Mvc;

namespace APSS.Web.Mvc.Controllers
{
    public class SeasonController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        // GET: SeasonController/Add a new Season
        public ActionResult Add()
        {
            return View();
        }

        // POST: SeasonController/Add a new Season
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(IFormCollection collection)
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

        // GET: SeasonController/Update Season
        public ActionResult Update(long seasonId)
        {
            return View();
        }

        // POST: SeasonController/Update Season
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(long seasonId, IFormCollection collection)
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

        // GET: SeasonController/Delete  Season
        public ActionResult Delete(long seasonId)
        {
            return View();
        }

        // POST: SeasonController/Delete Season
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long seasonId, IFormCollection collection)
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

        // GET: SeasonController/Get Season
        public ActionResult GetLand(long seasonId)
        {
            return View();
        }

        // POST: SeasonController/Get Season
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetLand(long seasonId, IFormCollection collection)
        {
            return View();
        }

        // GET: SeasonController/Get Seasons
        public ActionResult GetLands()
        {
            return View();
        }

        // POST: SeasonController/Get Seasons
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetLands(IFormCollection collection)
        {
            return View();
        }
    }
}