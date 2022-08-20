using APSS.Web.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace APSS.Web.Mvc.Controllers
{
    public class LandProductController : Controller
    {
        public IActionResult Index()
        {
            var landProductList = new LandAndLandProduct
            {
                landDto = new LandDto { Name = "land1", Id = 1, Address = "djskhao" },
                LandList = new List<LandDto>
                {
                    new LandDto{Name ="land1",Id=1,Address="djskhao"},
                    new LandDto{Name ="land2",Id=2,Address="djskhao2"}
                },
                ProductList = new List<LandProductDto>
                {
                    new LandProductDto{CropName="pro1", Id=1, HarvestEnd=DateTime.Now,Category="Baggash" },
                    new LandProductDto{CropName="pro2", Id=2, HarvestEnd=DateTime.Now,Category="farouq" },
                    new LandProductDto{CropName="pro3", Id=3, HarvestEnd=DateTime.Now,Category="ayman" },
                }
            };
            return View(landProductList);
        }

        // GET: LandProduc  tController/Add a new landProduct
        public ActionResult Add(long Id)
        {
            var landprodut = new LandProductDto
            {
                Seasons = new List<SeasonDto> {
                    new SeasonDto { Name = "season1", CreatedAt = DateTime.Now, Id = 1 },
                    new SeasonDto { Name = "season2", CreatedAt = DateTime.Now, Id = 2 },
                    new SeasonDto { Name = "season3", CreatedAt = DateTime.Now, Id = 3 },
                },
                Unit = new List<LandProductUnitDto>
                {
                    new LandProductUnitDto{Name ="Unit1", CreatedAt =DateTime.Now, Id=1},
                    new LandProductUnitDto{Name ="Unit2", CreatedAt =DateTime.Now, Id=2},
                    new LandProductUnitDto{Name ="Unit3", CreatedAt =DateTime.Now, Id=3},
                },
                ProducerId = Id,
            };

            return View(landprodut);
        }

        // POST: LandController/Add a new landProduct
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(LandProductDto landProductDto)
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

        // GET: LandProductController/_Add a new landProduct
        public ActionResult _Add(long landId)
        {
            var landProductWith = new LandAndLandProduct
            {
                landProductDto = new LandProductDto
                {
                    Seasons = new List<SeasonDto> {
                    new SeasonDto{ Name = "season1", CreatedAt = DateTime.Now, Id=1 },
                    new SeasonDto{ Name = "season2", CreatedAt = DateTime.Now, Id=2 },
                    new SeasonDto{ Name = "season3", CreatedAt = DateTime.Now, Id=3 },
                },
                    Unit = new List<LandProductUnitDto>
                {
                    new LandProductUnitDto{Name ="Unit1", CreatedAt =DateTime.Now, Id=1},
                    new LandProductUnitDto{Name ="Unit2", CreatedAt =DateTime.Now, Id=2},
                    new LandProductUnitDto{Name ="Unit3", CreatedAt =DateTime.Now, Id=3},
                }
                }
            };

            return View(landProductWith);
        }

        // POST: LandController/_Add a new landProduct
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _Add(LandProductAndSeasonAndUnit landProduct, IFormCollection collection)
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

        public ActionResult Details(long landProductId)
        {
            return View();
        }

        // GET: LandController/Update landProduct
        public ActionResult Update(long Id)
        {
            var landProductList = new LandAndLandProduct
            {
                landDto = new LandDto { Name = "land1", Id = 1, Address = "djskhao" },
                LandList = new List<LandDto>
                {
                    new LandDto{Name ="land1",Id=1,Address="djskhao"},
                    new LandDto{Name ="land2",Id=2,Address="djskhao2"}
                },
                ProductList = new List<LandProductDto>
                {
                    new LandProductDto{CropName="pro1", Id=1, HarvestEnd=DateTime.Now,Category="Baggash" },
                    new LandProductDto{CropName="pro2", Id=2, HarvestEnd=DateTime.Now,Category="farouq" },
                    new LandProductDto{CropName="pro3", Id=3, HarvestEnd=DateTime.Now,Category="ayman" },
                }
            };

            LandProductDto landProduct = landProductList.ProductList.Where(p => p.Id == Id).First();
            return View(landProduct);
        }

        // POST: LandController/Update landProduct
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(LandProductDto landProduct)
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

        // GET: LandController/Delete  landProduct
        public ActionResult Delete(long landProductId)
        {
            return View();
        }

        // POST: LandController/Delete landProduct
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete()
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

        // GET: LandController/Get landProduct
        public ActionResult GetLandProduct(long landProductId)
        {
            return View();
        }

        // POST: LandController/Get landProduct
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetLandProduct()
        {
            return View();
        }

        // GET: LandController/Get landProducts
        public ActionResult GetLandProducts(long landId)
        {
            return View();
        }

        // POST: LandController/Get landProducts
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetLandProducts()
        {
            return View();
        }
    }
}