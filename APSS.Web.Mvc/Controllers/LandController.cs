using APSS.Web.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace APSS.Web.Mvc.Controllers
{
    public class LandController : Controller
    {
        public async Task<IActionResult> Index()
        {
            LandAndProductUnit landAndUnit = new LandAndProductUnit
            {
                landDto = new LandDto { Name = "land1", Id = 1, Address = "djskhao" },
                LandList = new List<LandDto>
                {
                    new LandDto{Name ="land1",Id=1,Address="djskhao", Area =123},
                    new LandDto{Name ="land2",Id=2,Address="djskhao2", Area =321},
                    new LandDto{Name ="land3",Id=3,Address="djskhao3", Area =555},
                },
                ProductUnitDto = new LandProductUnitDto { Name = "P1", Id = 1 },
                ProductUnitList = new List<LandProductUnitDto>
                {
                    new LandProductUnitDto{Name ="P1", Id=1},
                    new LandProductUnitDto{Name ="P2", Id=2}
                }
            };
            List<LandDto> landDto = new List<LandDto>();

            return View(landAndUnit);
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

        public ActionResult Details(long Id)
        {
            var landAndProduct = new LandAndLandProduct
            {
                landDto = new LandDto { Name = "land1", Id = 1, Address = "djskhao" },
                LandList = new List<LandDto>
                {
                    new LandDto{Name ="land1",Id=1,Address="djskhao", Area =123},
                    new LandDto{Name ="land2",Id=2,Address="djskhao2", Area =321},
                    new LandDto{Name ="land3",Id=3,Address="djskhao3", Area =555},
                }
            };

            landAndProduct.landDto = landAndProduct.LandList.Where(i => i.Id == Id).First();
            landAndProduct.landDto.OwnedBy = new UserDto
            {
                Name = "Farouq"
            };
            return View(landAndProduct);
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
        public ActionResult Delete(long landId)
        {
            return View();
        }

        // POST: LandController/Delete land
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long landId, IFormCollection collection)
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