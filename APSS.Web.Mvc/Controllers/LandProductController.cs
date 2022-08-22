using APSS.Web.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace APSS.Web.Mvc.Controllers
{
    public class LandProductController : Controller
    {
        public IActionResult Index()
        {
            var LandList = new List<LandDto>
                {
                    new LandDto{Name ="land1",Id=1,Address="djskhao", Area =123},
                    new LandDto{Name ="land2",Id=2,Address="djskhao2", Area =321},
                    new LandDto{Name ="land3",Id=3,Address="djskhao3", Area =555},
                    new LandDto{Name ="land4",Id=4,Address="djskhao3", Area =555},
                };
            var ProductList = new List<LandProductDto>
                {
                    new LandProductDto{CropName="pro1", Id=1, HarvestEnd=DateTime.Now,Category="Baggash", Producer=LandList[0] },
                    new LandProductDto{CropName="pro2", Id=2, HarvestEnd=DateTime.Now,Category="Baggash", Producer=LandList[1] },
                    new LandProductDto{CropName="pro3", Id=3, HarvestEnd=DateTime.Now,Category="Baggash", Producer=LandList[2] },
                    new LandProductDto{CropName="pro4", Id=4, HarvestEnd=DateTime.Now,Category="farouq", Producer=LandList[0] },
                    new LandProductDto{CropName="pro5", Id=5, HarvestEnd=DateTime.Now,Category="farouq", Producer=LandList[1] },
                    new LandProductDto{CropName="pro6", Id=6, HarvestEnd=DateTime.Now,Category="farouq", Producer=LandList[2] },
                    new LandProductDto{CropName="pro7", Id=7, HarvestEnd=DateTime.Now,Category="ayman", Producer=LandList[0] },
                    new LandProductDto{CropName="pro8", Id=8, HarvestEnd=DateTime.Now,Category="ayman", Producer=LandList[1] },
                    new LandProductDto{CropName="pro9", Id=9, HarvestEnd=DateTime.Now,Category="ayman", Producer=LandList[2] },
                };
            return View(ProductList);
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
                Units = new List<LandProductUnitDto>
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
        public ActionResult XsAdd(long landId)
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
                    Units = new List<LandProductUnitDto>
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

        public ActionResult Details(long Id)
        {
            var ProductList = new List<LandProductDto>
                {
                    new LandProductDto{CropName="pro1", Id=1, HarvestEnd=DateTime.Now,Category="Baggash" },
                    new LandProductDto{CropName="pro2", Id=2, HarvestEnd=DateTime.Now,Category="farouq" },
                    new LandProductDto{CropName="pro3", Id=3, HarvestEnd=DateTime.Now,Category="ayman" },
                };
            var landProduct = ProductList.Where(i => i.Id == Id).FirstOrDefault();
            return View(landProduct);
        }

        // GET: LandController/Update landProduct
        public ActionResult Update(long Id)
        {
            var ProductList = new List<LandProductDto>
                {
                    new LandProductDto{CropName="pro1", Id=1, HarvestEnd=DateTime.Now,Category="Baggash"},
                    new LandProductDto{CropName="pro2", Id=2, HarvestEnd=DateTime.Now,Category="Baggash" },
                    new LandProductDto{CropName="pro3", Id=3, HarvestEnd=DateTime.Now,Category="Baggash" },
                    new LandProductDto{CropName="pro4", Id=4, HarvestEnd=DateTime.Now,Category="farouq" },
                    new LandProductDto{CropName="pro5", Id=5, HarvestEnd=DateTime.Now,Category="farouq" },
                    new LandProductDto{CropName="pro6", Id=6, HarvestEnd=DateTime.Now,Category="farouq"},
                    new LandProductDto{CropName="pro7", Id=7, HarvestEnd=DateTime.Now,Category="ayman" },
                    new LandProductDto{CropName="pro8", Id=8, HarvestEnd=DateTime.Now,Category="ayman" },
                    new LandProductDto{CropName="pro9", Id=9, HarvestEnd=DateTime.Now,Category="ayman" },
                };

            LandProductDto landProduct = ProductList.Where(p => p.Id == Id).First();
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
        public ActionResult Delete(long Id)
        {
            var ProductList = new List<LandProductDto>
                {
                    new LandProductDto{CropName="pro1", Id=1, HarvestEnd=DateTime.Now,Category="Baggash" },
                    new LandProductDto{CropName="pro2", Id=2, HarvestEnd=DateTime.Now,Category="farouq" },
                    new LandProductDto{CropName="pro3", Id=3, HarvestEnd=DateTime.Now,Category="ayman" },
                };
            var landProduct = ProductList.Where(i => i.Id == Id).FirstOrDefault();
            return View(landProduct);
        }

        // POST: LandController/Delete landProduct
        [HttpPost]
        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeletePost(long Id)
        {
            var ProductList = new List<LandProductDto>
                {
                    new LandProductDto{CropName="pro1", Id=1, HarvestEnd=DateTime.Now,Category="Baggash" },
                    new LandProductDto{CropName="pro2", Id=2, HarvestEnd=DateTime.Now,Category="farouq" },
                    new LandProductDto{CropName="pro3", Id=3, HarvestEnd=DateTime.Now,Category="ayman" },
                };
            var landProduct = ProductList.Where(i => i.Id == Id).FirstOrDefault();
            ProductList.Remove(landProduct!);
            return RedirectToAction(nameof(Index));
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
        public ActionResult GetLandProducts(long Id)
        {
            var LandList = new List<LandDto>
                {
                    new LandDto{Name ="land1",Id=1,Address="djskhao", Area =123},
                    new LandDto{Name ="land2",Id=2,Address="djskhao2", Area =321},
                    new LandDto{Name ="land3",Id=3,Address="djskhao3", Area =555},
                    new LandDto{Name ="land4",Id=4,Address="djskhao3", Area =555},
                };
            var ProductList = new List<LandProductDto>
                {
                    new LandProductDto{CropName="pro1", Id=1, HarvestEnd=DateTime.Now,Category="Baggash", Producer=LandList[0] },
                    new LandProductDto{CropName="pro1", Id=2, HarvestEnd=DateTime.Now,Category="Baggash", Producer=LandList[1] },
                    new LandProductDto{CropName="pro1", Id=3, HarvestEnd=DateTime.Now,Category="Baggash", Producer=LandList[2] },
                    new LandProductDto{CropName="pro2", Id=4, HarvestEnd=DateTime.Now,Category="farouq", Producer=LandList[0] },
                    new LandProductDto{CropName="pro2", Id=5, HarvestEnd=DateTime.Now,Category="farouq", Producer=LandList[1] },
                    new LandProductDto{CropName="pro2", Id=6, HarvestEnd=DateTime.Now,Category="farouq", Producer=LandList[2] },
                    new LandProductDto{CropName="pro3", Id=7, HarvestEnd=DateTime.Now,Category="ayman", Producer=LandList[0] },
                    new LandProductDto{CropName="pro3", Id=8, HarvestEnd=DateTime.Now,Category="ayman", Producer=LandList[1] },
                    new LandProductDto{CropName="pro3", Id=9, HarvestEnd=DateTime.Now,Category="ayman", Producer=LandList[2] },
                };
            if (LandList.Count >= Id)
            {
                return View(ProductList.Where(l => l.Producer.Id == Id).ToList());
            }
            else
            {
                return View("ErrorPage", "Out of range");
            }
        }
    }
}