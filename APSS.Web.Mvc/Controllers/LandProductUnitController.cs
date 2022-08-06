using Microsoft.AspNetCore.Mvc;

namespace APSS.Web.Mvc.Controllers
{
    public class LandProductUnitController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        // GET: LandProductUnitController/Add a new LandProductUnit
        public ActionResult Add()
        {
            return View();
        }

        // POST: LandProductUnitController/Add a new LandProductUnit
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

        // GET: LandProductUnitController/Update LandProductUnit
        public ActionResult Update(long landProductUnitId)
        {
            return View();
        }

        // POST: LandProductUnit/Update LandProductUnit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(long landProductUnitId, IFormCollection collection)
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

        // GET: LandProductUnitController/Delete  LandProductUnit
        public ActionResult Delete(long landProductUnitId)
        {
            return View();
        }

        // POST: LandProductUnitController/Delete LandProductUnit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long landProductUnitId, IFormCollection collection)
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

        // GET: LandProductUnitController/Get LandProductUnit
        public ActionResult GetLandProductUnit(long landProductUnitId)
        {
            return View();
        }

        // POST: LandProductUnitController/Get LandProductUnit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetLandProductUnit(long landProductUnitId, IFormCollection collection)
        {
            return View();
        }

        // GET: LandProductUnitController/Get LandProductUnits
        public ActionResult GetLandProductUnits()
        {
            return View();
        }

        // POST: LandProductUnitController/Get LandProductUnits
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetLandProductUnits(IFormCollection collection)
        {
            return View();
        }
    }
}