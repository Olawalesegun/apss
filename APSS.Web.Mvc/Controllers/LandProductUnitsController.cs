using APSS.Web.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace APSS.Web.Mvc.Controllers
{
    public class LandProductUnitsController : Controller
    {
        public IActionResult Index()
        {
            var unitList = new List<LandProductUnitDto>
            {
                new LandProductUnitDto{Id=1, Name ="unit1", CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(2)},
                new LandProductUnitDto{Id=2, Name ="unit2", CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(2)},
                new LandProductUnitDto{Id=3, Name ="unit3", CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(2)},
                new LandProductUnitDto{Id=4, Name ="unit4", CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(2)},
            };
            return View(unitList);
        }

        // GET: LandProductUnitController/Add a new LandProductUnit
        public ActionResult Add()
        {
            return View();
        }

        // POST: LandProductUnitController/Add a new LandProductUnit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(LandProductUnitDto landProductUnit)
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
        public ActionResult Update(long Id)
        {
            var unitList = new List<LandProductUnitDto>
            {
                new LandProductUnitDto{Id=1, Name ="unit1", CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(2)},
                new LandProductUnitDto{Id=2, Name ="unit2", CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(2)},
                new LandProductUnitDto{Id=3, Name ="unit3", CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(2)},
                new LandProductUnitDto{Id=4, Name ="unit4", CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(2)},
            };
            return View(unitList.Where(i => i.Id == Id).First());
        }

        // POST: LandProductUnit/Update LandProductUnit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(LandProductUnitDto landProductUnit)
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
        public ActionResult Delete(long Id)
        {
            var unitList = new List<LandProductUnitDto>
            {
                new LandProductUnitDto{Id=1, Name ="unit1", CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(2)},
                new LandProductUnitDto{Id=2, Name ="unit2", CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(2)},
                new LandProductUnitDto{Id=3, Name ="unit3", CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(2)},
                new LandProductUnitDto{Id=4, Name ="unit4", CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(2)},
            };
            return View(unitList.Where(i => i.Id == Id).First());
        }

        // POST: LandProductUnitController/Delete LandProductUnit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(LandProductUnitDto landProductUnit)
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
        public ActionResult GetLandProductUnit(long Id)
        {
            var unitList = new List<LandProductUnitDto>
            {
                new LandProductUnitDto{Id=1, Name ="unit1", CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(2)},
                new LandProductUnitDto{Id=2, Name ="unit2", CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(2)},
                new LandProductUnitDto{Id=3, Name ="unit3", CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(2)},
                new LandProductUnitDto{Id=4, Name ="unit4", CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(2)},
            };
            return View(unitList.Where(i => i.Id == Id).First());
        }
    }
}