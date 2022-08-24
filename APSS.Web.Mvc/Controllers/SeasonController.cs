using APSS.Web.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace APSS.Web.Mvc.Controllers
{
    public class SeasonController : Controller
    {
        public IActionResult Index()
        {
            var seasonList = new List<SeasonDto>
            {
                new SeasonDto { Id = 1, Name ="Season1", StartsAt=DateTime.Now, EndsAt=DateTime.Now.AddDays(2), CreatedAt=DateTime.Now, ModifiedAt=DateTime.Now.AddDays(1)},
                new SeasonDto { Id = 2, Name ="Season2", StartsAt=DateTime.Now, EndsAt=DateTime.Now.AddDays(2), CreatedAt=DateTime.Now, ModifiedAt=DateTime.Now.AddDays(1)},
                new SeasonDto { Id = 3, Name ="Season3", StartsAt=DateTime.Now, EndsAt=DateTime.Now.AddDays(2), CreatedAt=DateTime.Now, ModifiedAt=DateTime.Now.AddDays(1)},
                new SeasonDto { Id = 4, Name ="Season4", StartsAt=DateTime.Now, EndsAt=DateTime.Now.AddDays(2), CreatedAt=DateTime.Now, ModifiedAt=DateTime.Now.AddDays(1)},
            };
            return View(seasonList);
        }

        // GET: SeasonController/Add a new Season
        public ActionResult Add()
        {
            return View();
        }

        // POST: SeasonController/Add a new Season
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(SeasonDto season)
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
        public ActionResult Update(long Id)
        {
            var seasonList = new List<SeasonDto>
            {
                new SeasonDto { Id = 1, Name ="Season1", StartsAt=DateTime.Now, EndsAt=DateTime.Now.AddDays(2), CreatedAt=DateTime.Now, ModifiedAt=DateTime.Now.AddDays(1)},
                new SeasonDto { Id = 2, Name ="Season2", StartsAt=DateTime.Now, EndsAt=DateTime.Now.AddDays(2), CreatedAt=DateTime.Now, ModifiedAt=DateTime.Now.AddDays(1)},
                new SeasonDto { Id = 3, Name ="Season3", StartsAt=DateTime.Now, EndsAt=DateTime.Now.AddDays(2), CreatedAt=DateTime.Now, ModifiedAt=DateTime.Now.AddDays(1)},
                new SeasonDto { Id = 4, Name ="Season4", StartsAt=DateTime.Now, EndsAt=DateTime.Now.AddDays(2), CreatedAt=DateTime.Now, ModifiedAt=DateTime.Now.AddDays(1)},
            };

            return View(seasonList.Where(i => i.Id == Id).First());
        }

        // POST: SeasonController/Update Season
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(SeasonDto season)
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
        public ActionResult Delete(long Id)
        {
            var seasonList = new List<SeasonDto>
            {
                new SeasonDto { Id = 1, Name ="Season1", StartsAt=DateTime.Now, EndsAt=DateTime.Now.AddDays(2), CreatedAt=DateTime.Now, ModifiedAt=DateTime.Now.AddDays(1)},
                new SeasonDto { Id = 2, Name ="Season2", StartsAt=DateTime.Now, EndsAt=DateTime.Now.AddDays(2), CreatedAt=DateTime.Now, ModifiedAt=DateTime.Now.AddDays(1)},
                new SeasonDto { Id = 3, Name ="Season3", StartsAt=DateTime.Now, EndsAt=DateTime.Now.AddDays(2), CreatedAt=DateTime.Now, ModifiedAt=DateTime.Now.AddDays(1)},
                new SeasonDto { Id = 4, Name ="Season4", StartsAt=DateTime.Now, EndsAt=DateTime.Now.AddDays(2), CreatedAt=DateTime.Now, ModifiedAt=DateTime.Now.AddDays(1)},
            };

            return View(seasonList.Where(i => i.Id == Id).First());
        }

        // POST: SeasonController/Delete Season
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(SeasonDto season)
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
        public ActionResult GetSeason(long Id)
        {
            return View();
        }
    }
}