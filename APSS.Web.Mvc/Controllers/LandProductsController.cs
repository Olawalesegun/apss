using APSS.Web.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace APSS.Web.Mvc.Controllers
{
    public class LandProductsController : Controller
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
            var landProductDto = new LandProductDto
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
            };

            return View(landProductDto);
        }

        // POST: LandController/_Add a new landProduct
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _Add(LandProductDto landProduct, IFormCollection collection)
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
                    new LandProductDto{CropName="pro1", Id=1, HarvestEnd=DateTime.Now,Category="Baggash", AddedBy = new UserDto{Name = "User1"},ProducedIn =new SeasonDto{ Name = "season1", CreatedAt = DateTime.Now, Id=1 }, Producer =new LandDto{Name ="land1",Id=1,Address="djskhao", Area =123}},
                    new LandProductDto{CropName="pro2", Id=2, HarvestEnd=DateTime.Now,Category="Baggash",AddedBy = new UserDto{Name = "User2"},ProducedIn =new SeasonDto{ Name = "season2", CreatedAt = DateTime.Now, Id=2 }, Producer =new LandDto{Name ="land2",Id=2,Address="djskhao", Area =123}},
                    new LandProductDto{CropName="pro3", Id=3, HarvestEnd=DateTime.Now,Category="Baggash",AddedBy = new UserDto{Name = "User3"},ProducedIn =new SeasonDto{ Name = "season3", CreatedAt = DateTime.Now, Id=3 }, Producer =new LandDto{Name ="land3",Id=3,Address="djskhao", Area =123}},
                    new LandProductDto{CropName="pro4", Id=4, HarvestEnd=DateTime.Now,Category="farouq" ,AddedBy = new UserDto{Name = "User4"},ProducedIn =new SeasonDto{ Name = "season4", CreatedAt = DateTime.Now, Id=4 }, Producer =new LandDto{Name ="land4",Id=4,Address="djskhao", Area =123}},
                    new LandProductDto{CropName="pro5", Id=5, HarvestEnd=DateTime.Now,Category="farouq" ,AddedBy = new UserDto{Name = "User5"},ProducedIn =new SeasonDto{ Name = "season5", CreatedAt = DateTime.Now, Id=5 }, Producer =new LandDto{Name ="land5",Id=5,Address="djskhao", Area =123}},
                    new LandProductDto{CropName="pro6", Id=6, HarvestEnd=DateTime.Now,Category="farouq",AddedBy = new UserDto{Name = "User6"},ProducedIn =new SeasonDto{ Name = "season6", CreatedAt = DateTime.Now, Id=6 }, Producer =new LandDto{Name ="land6",Id=6,Address="djskhao", Area =123}},
                    new LandProductDto{CropName="pro7", Id=7, HarvestEnd=DateTime.Now,Category="ayman" ,AddedBy = new UserDto{Name = "User7"},ProducedIn =new SeasonDto{ Name = "season7", CreatedAt = DateTime.Now, Id=7 }, Producer =new LandDto{Name ="land7",Id=7,Address="djskhao", Area =123}},
                    new LandProductDto{CropName="pro8", Id=8, HarvestEnd=DateTime.Now,Category="ayman" ,AddedBy = new UserDto{Name = "User8"},ProducedIn =new SeasonDto{ Name = "season8", CreatedAt = DateTime.Now, Id=8 }, Producer =new LandDto{Name ="land8",Id=8,Address="djskhao", Area =123}},
                    new LandProductDto{CropName="pro9", Id=9, HarvestEnd=DateTime.Now,Category="ayman" ,AddedBy = new UserDto{Name = "User9"},ProducedIn =new SeasonDto{ Name = "season9", CreatedAt = DateTime.Now, Id=9 }, Producer =new LandDto{Name ="land9",Id=9,Address="djskhao", Area =123}},
                };
            var landProduct = ProductList.Where(i => i.Id == Id).FirstOrDefault();
            return View(landProduct);
        }

        // GET: LandController/Update landProduct
        public ActionResult Update(long Id)
        {
            var ProductList = new List<LandProductDto>
                {
                    new LandProductDto{CropName="pro1", Id=1, HarvestEnd=DateTime.Now,Category="Baggash", AddedBy = new UserDto{Name = "User1"},ProducedIn =new SeasonDto{ Name = "season1", CreatedAt = DateTime.Now, Id=1 }, Producer =new LandDto{Name ="land1",Id=1,Address="djskhao", Area =123}, Seasons = new List<SeasonDto> {new SeasonDto{ Name = "season1", CreatedAt = DateTime.Now, Id=1 },new SeasonDto{ Name = "season2", CreatedAt = DateTime.Now, Id=2 },new SeasonDto{ Name = "season3", CreatedAt = DateTime.Now, Id=3 },},Units = new List<LandProductUnitDto>{new LandProductUnitDto{Name ="Unit1", CreatedAt =DateTime.Now, Id=1},new LandProductUnitDto{Name ="Unit2", CreatedAt =DateTime.Now, Id=2},new LandProductUnitDto{Name ="Unit3", CreatedAt =DateTime.Now, Id=3},}},
                    new LandProductDto{CropName="pro2", Id=2, HarvestEnd=DateTime.Now,Category="Baggash",AddedBy = new UserDto{Name = "User2"},ProducedIn =new SeasonDto{ Name = "season2", CreatedAt = DateTime.Now, Id=2 }, Producer =new LandDto{Name ="land2",Id=2,Address="djskhao", Area =123}, Seasons = new List<SeasonDto> {new SeasonDto{ Name = "season1", CreatedAt = DateTime.Now, Id=1 },new SeasonDto{ Name = "season2", CreatedAt = DateTime.Now, Id=2 },new SeasonDto{ Name = "season3", CreatedAt = DateTime.Now, Id=3 },},Units = new List<LandProductUnitDto>{new LandProductUnitDto{Name ="Unit1", CreatedAt =DateTime.Now, Id=1},new LandProductUnitDto{Name ="Unit2", CreatedAt =DateTime.Now, Id=2},new LandProductUnitDto{Name ="Unit3", CreatedAt =DateTime.Now, Id=3},}},
                    new LandProductDto{CropName="pro3", Id=3, HarvestEnd=DateTime.Now,Category="Baggash",AddedBy = new UserDto{Name = "User3"},ProducedIn =new SeasonDto{ Name = "season3", CreatedAt = DateTime.Now, Id=3 }, Producer =new LandDto{Name ="land3",Id=3,Address="djskhao", Area =123}, Seasons = new List<SeasonDto> {new SeasonDto{ Name = "season1", CreatedAt = DateTime.Now, Id=1 },new SeasonDto{ Name = "season2", CreatedAt = DateTime.Now, Id=2 },new SeasonDto{ Name = "season3", CreatedAt = DateTime.Now, Id=3 },},Units = new List<LandProductUnitDto>{new LandProductUnitDto{Name ="Unit1", CreatedAt =DateTime.Now, Id=1},new LandProductUnitDto{Name ="Unit2", CreatedAt =DateTime.Now, Id=2},new LandProductUnitDto{Name ="Unit3", CreatedAt =DateTime.Now, Id=3},}},
                    new LandProductDto{CropName="pro4", Id=4, HarvestEnd=DateTime.Now,Category="farouq" ,AddedBy = new UserDto{Name = "User4"},ProducedIn =new SeasonDto{ Name = "season4", CreatedAt = DateTime.Now, Id=4 }, Producer =new LandDto{Name ="land4",Id=4,Address="djskhao", Area =123}, Seasons = new List<SeasonDto> {new SeasonDto{ Name = "season1", CreatedAt = DateTime.Now, Id=1 },new SeasonDto{ Name = "season2", CreatedAt = DateTime.Now, Id=2 },new SeasonDto{ Name = "season3", CreatedAt = DateTime.Now, Id=3 },},Units = new List<LandProductUnitDto>{new LandProductUnitDto{Name ="Unit1", CreatedAt =DateTime.Now, Id=1},new LandProductUnitDto{Name ="Unit2", CreatedAt =DateTime.Now, Id=2},new LandProductUnitDto{Name ="Unit3", CreatedAt =DateTime.Now, Id=3},}},
                    new LandProductDto{CropName="pro5", Id=5, HarvestEnd=DateTime.Now,Category="farouq" ,AddedBy = new UserDto{Name = "User5"},ProducedIn =new SeasonDto{ Name = "season5", CreatedAt = DateTime.Now, Id=5 }, Producer =new LandDto{Name ="land5",Id=5,Address="djskhao", Area =123}, Seasons = new List<SeasonDto> {new SeasonDto{ Name = "season1", CreatedAt = DateTime.Now, Id=1 },new SeasonDto{ Name = "season2", CreatedAt = DateTime.Now, Id=2 },new SeasonDto{ Name = "season3", CreatedAt = DateTime.Now, Id=3 },},Units = new List<LandProductUnitDto>{new LandProductUnitDto{Name ="Unit1", CreatedAt =DateTime.Now, Id=1},new LandProductUnitDto{Name ="Unit2", CreatedAt =DateTime.Now, Id=2},new LandProductUnitDto{Name ="Unit3", CreatedAt =DateTime.Now, Id=3},}},
                    new LandProductDto{CropName="pro6", Id=6, HarvestEnd=DateTime.Now,Category="farouq",AddedBy = new UserDto{Name = "User6"},ProducedIn =new SeasonDto{ Name = "season6", CreatedAt = DateTime.Now, Id=6 }, Producer =new LandDto{Name ="land6",Id=6,Address="djskhao", Area =123}, Seasons = new List<SeasonDto> {new SeasonDto{ Name = "season1", CreatedAt = DateTime.Now, Id=1 },new SeasonDto{ Name = "season2", CreatedAt = DateTime.Now, Id=2 },new SeasonDto{ Name = "season3", CreatedAt = DateTime.Now, Id=3 },},Units = new List<LandProductUnitDto>{new LandProductUnitDto{Name ="Unit1", CreatedAt =DateTime.Now, Id=1},new LandProductUnitDto{Name ="Unit2", CreatedAt =DateTime.Now, Id=2},new LandProductUnitDto{Name ="Unit3", CreatedAt =DateTime.Now, Id=3},}},
                    new LandProductDto{CropName="pro7", Id=7, HarvestEnd=DateTime.Now,Category="ayman" ,AddedBy = new UserDto{Name = "User7"},ProducedIn =new SeasonDto{ Name = "season7", CreatedAt = DateTime.Now, Id=7 }, Producer =new LandDto{Name ="land7",Id=7,Address="djskhao", Area =123}, Seasons = new List<SeasonDto> {new SeasonDto{ Name = "season1", CreatedAt = DateTime.Now, Id=1 },new SeasonDto{ Name = "season2", CreatedAt = DateTime.Now, Id=2 },new SeasonDto{ Name = "season3", CreatedAt = DateTime.Now, Id=3 },},Units = new List<LandProductUnitDto>{new LandProductUnitDto{Name ="Unit1", CreatedAt =DateTime.Now, Id=1},new LandProductUnitDto{Name ="Unit2", CreatedAt =DateTime.Now, Id=2},new LandProductUnitDto{Name ="Unit3", CreatedAt =DateTime.Now, Id=3},}},
                    new LandProductDto{CropName="pro8", Id=8, HarvestEnd=DateTime.Now,Category="ayman" ,AddedBy = new UserDto{Name = "User8"},ProducedIn =new SeasonDto{ Name = "season8", CreatedAt = DateTime.Now, Id=8 }, Producer =new LandDto{Name ="land8",Id=8,Address="djskhao", Area =123}, Seasons = new List<SeasonDto> {new SeasonDto{ Name = "season1", CreatedAt = DateTime.Now, Id=1 },new SeasonDto{ Name = "season2", CreatedAt = DateTime.Now, Id=2 },new SeasonDto{ Name = "season3", CreatedAt = DateTime.Now, Id=3 },},Units = new List<LandProductUnitDto>{new LandProductUnitDto{Name ="Unit1", CreatedAt =DateTime.Now, Id=1},new LandProductUnitDto{Name ="Unit2", CreatedAt =DateTime.Now, Id=2},new LandProductUnitDto{Name ="Unit3", CreatedAt =DateTime.Now, Id=3},}},
                    new LandProductDto{CropName="pro9", Id=9, HarvestEnd=DateTime.Now,Category="ayman" ,AddedBy = new UserDto{Name = "User9"},ProducedIn =new SeasonDto{ Name = "season9", CreatedAt = DateTime.Now, Id=9 }, Producer =new LandDto{Name ="land9",Id=9,Address="djskhao", Area =123}, Seasons = new List<SeasonDto> {new SeasonDto{ Name = "season1", CreatedAt = DateTime.Now, Id=1 },new SeasonDto{ Name = "season2", CreatedAt = DateTime.Now, Id=2 },new SeasonDto{ Name = "season3", CreatedAt = DateTime.Now, Id=3 },},Units = new List<LandProductUnitDto>{new LandProductUnitDto{Name ="Unit1", CreatedAt =DateTime.Now, Id=1},new LandProductUnitDto{Name ="Unit2", CreatedAt =DateTime.Now, Id=2},new LandProductUnitDto{Name ="Unit3", CreatedAt =DateTime.Now, Id=3},}},
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
                    new LandProductDto{CropName="pro1", Id=1, HarvestEnd=DateTime.Now,Category="Baggash", AddedBy = new UserDto{Name = "User1"},ProducedIn =new SeasonDto{ Name = "season1", CreatedAt = DateTime.Now, Id=1 }, Producer =new LandDto{Name ="land1",Id=1,Address="djskhao", Area =123}},
                    new LandProductDto{CropName="pro2", Id=2, HarvestEnd=DateTime.Now,Category="Baggash",AddedBy = new UserDto{Name = "User2"},ProducedIn =new SeasonDto{ Name = "season2", CreatedAt = DateTime.Now, Id=2 }, Producer =new LandDto{Name ="land2",Id=2,Address="djskhao", Area =123}},
                    new LandProductDto{CropName="pro3", Id=3, HarvestEnd=DateTime.Now,Category="Baggash",AddedBy = new UserDto{Name = "User3"},ProducedIn =new SeasonDto{ Name = "season3", CreatedAt = DateTime.Now, Id=3 }, Producer =new LandDto{Name ="land3",Id=3,Address="djskhao", Area =123}},
                    new LandProductDto{CropName="pro4", Id=4, HarvestEnd=DateTime.Now,Category="farouq" ,AddedBy = new UserDto{Name = "User4"},ProducedIn =new SeasonDto{ Name = "season4", CreatedAt = DateTime.Now, Id=4 }, Producer =new LandDto{Name ="land4",Id=4,Address="djskhao", Area =123}},
                    new LandProductDto{CropName="pro5", Id=5, HarvestEnd=DateTime.Now,Category="farouq" ,AddedBy = new UserDto{Name = "User5"},ProducedIn =new SeasonDto{ Name = "season5", CreatedAt = DateTime.Now, Id=5 }, Producer =new LandDto{Name ="land5",Id=5,Address="djskhao", Area =123}},
                    new LandProductDto{CropName="pro6", Id=6, HarvestEnd=DateTime.Now,Category="farouq",AddedBy = new UserDto{Name = "User6"},ProducedIn =new SeasonDto{ Name = "season6", CreatedAt = DateTime.Now, Id=6 }, Producer =new LandDto{Name ="land6",Id=6,Address="djskhao", Area =123}},
                    new LandProductDto{CropName="pro7", Id=7, HarvestEnd=DateTime.Now,Category="ayman" ,AddedBy = new UserDto{Name = "User7"},ProducedIn =new SeasonDto{ Name = "season7", CreatedAt = DateTime.Now, Id=7 }, Producer =new LandDto{Name ="land7",Id=7,Address="djskhao", Area =123}},
                    new LandProductDto{CropName="pro8", Id=8, HarvestEnd=DateTime.Now,Category="ayman" ,AddedBy = new UserDto{Name = "User8"},ProducedIn =new SeasonDto{ Name = "season8", CreatedAt = DateTime.Now, Id=8 }, Producer =new LandDto{Name ="land8",Id=8,Address="djskhao", Area =123}},
                    new LandProductDto{CropName="pro9", Id=9, HarvestEnd=DateTime.Now,Category="ayman" ,AddedBy = new UserDto{Name = "User9"},ProducedIn =new SeasonDto{ Name = "season9", CreatedAt = DateTime.Now, Id=9 }, Producer =new LandDto{Name ="land9",Id=9,Address="djskhao", Area =123}},
                };

            var landProduct = ProductList.Where(i => i.Id == Id).First();
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
                    new LandProductDto{CropName="pro2", Id=2, HarvestEnd=DateTime.Now,Category="Baggash", Producer=LandList[1] },
                    new LandProductDto{CropName="pro3", Id=3, HarvestEnd=DateTime.Now,Category="Baggash", Producer=LandList[2] },
                    new LandProductDto{CropName="pro4", Id=4, HarvestEnd=DateTime.Now,Category="farouq", Producer=LandList[0] },
                    new LandProductDto{CropName="pro5", Id=5, HarvestEnd=DateTime.Now,Category="farouq", Producer=LandList[3] },
                    new LandProductDto{CropName="pro6", Id=6, HarvestEnd=DateTime.Now,Category="farouq", Producer=LandList[2] },
                    new LandProductDto{CropName="pro7", Id=7, HarvestEnd=DateTime.Now,Category="ayman", Producer=LandList[3] },
                    new LandProductDto{CropName="pro8", Id=8, HarvestEnd=DateTime.Now,Category="ayman", Producer=LandList[3] },
                    new LandProductDto{CropName="pro9", Id=9, HarvestEnd=DateTime.Now,Category="ayman", Producer=LandList[2] },
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