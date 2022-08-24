using APSS.Web.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace APSS.Web.Mvc.Controllers
{
    public class LandController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var LandList = new List<LandDto>
                {
                    new LandDto{Name ="land1",Id=1,Address="djskhao", Area =123},
                    new LandDto{Name ="land2",Id=2,Address="djskhao2", Area =321},
                    new LandDto{Name ="land3",Id=3,Address="djskhao3", Area =555},
                    new LandDto{Name ="land4",Id=4,Address="djskhao3", Area =555},
                };

            return View(LandList);
        }

        // GET: LandController/Add a new land
        public ActionResult Add()
        {
            var land = new LandDto();
            return View(land);
        }

        // POST: LandController/Add a new land
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(LandDto newLand)
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

        public ActionResult Details(long Id)
        {
            var LandList = new List<LandDto>
                {
                    new LandDto{Name ="land1",Id=1,Address="djskhao", Area =123},
                    new LandDto{Name ="land2",Id=2,Address="djskhao2", Area =321},
                    new LandDto{Name ="land3",Id=3,Address="djskhao3", Area =555},
                    new LandDto{Name ="land4",Id=4,Address="djskhao3", Area =555},
                };

            LandDto land = LandList.Where(i => i.Id == Id).First();
            land.OwnedBy = new UserDto
            {
                Name = "Farouq"
            };
            return View(land);
        }

        // GET: LandController/Update land
        public ActionResult Update(long Id)
        {
            var landAndProduct = new LandAndLandProduct
            {
                landDto = new LandDto { Name = "land1", Id = 1, Address = "djskhao" },
                LandList = new List<LandDto>
                {
                    new LandDto{Name ="land1",Id=1,Address="djskhao", Area =123, Latitude=123, Longitude=456, IsUsable=false, IsUsed=false},
                    new LandDto{Name ="land2",Id=2,Address="djskhao2", Area =321, Latitude=123, Longitude=456, IsUsable=false, IsUsed=false},
                    new LandDto{Name ="land3",Id=3,Address="djskhao3", Area =555, Latitude=123, Longitude=456, IsUsable=false, IsUsed=false},
                }
            };

            LandDto land = landAndProduct.LandList.Where(i => i.Id == Id).First();
            return View(land);
        }

        // POST: LandController/Update land
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(LandDto landDto)
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

        // GET: LandController/Delete  land
        public ActionResult Delete(long Id)
        {
            var LandList = new List<LandDto>
                {
                    new LandDto{Name ="land1",Id=1,Address="djskhao", Area =123},
                    new LandDto{Name ="land2",Id=2,Address="djskhao2", Area =321},
                    new LandDto{Name ="land3",Id=3,Address="djskhao3", Area =555},
                };

            LandDto land = LandList.Where(i => i.Id == Id).First();
            land.OwnedBy = new UserDto
            {
                Name = "Farouq"
            };

            return View(land);
        }

        // POST: LandController/Delete land
        [HttpPost]
        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeletePost(long Id)
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

        // GET: LandController/Get land
        public ActionResult GetLand(long landId)
        {
            return View();
        }

        // POST: LandController/Get land
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetLand(long landId, IFormCollection collection)
        {
            return View();
        }

        // GET: LandController/Get lands
        public ActionResult GetLands(long userId)
        {
            return View();
        }

        // POST: LandController/Get lands
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetLands(long userId, IFormCollection collection)
        {
            return View();
        }
    }
}