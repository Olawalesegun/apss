using Microsoft.AspNetCore.Mvc;
using APSS.Web.Dtos;

namespace APSS.Web.Mvc.Areas.Controllers
{
    [Area(Areas.Animals)]
    public class ProductExpensesController : Controller
    {
        public IActionResult Index(long Id)
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
            var productExpenses = new List<ProductExpenseDto>
            {
                new ProductExpenseDto{Type = "type1", Id = 1, Price = 2134, SpentOn = ProductList[0]},
                new ProductExpenseDto{Type = "type2", Id = 2, Price = 2134, SpentOn = ProductList[0]},
                new ProductExpenseDto{Type = "type3", Id = 3, Price = 2134, SpentOn = ProductList[1]},
                new ProductExpenseDto{Type = "type4", Id = 4, Price = 2134, SpentOn = ProductList[1]},
                new ProductExpenseDto{Type = "type5", Id = 5, Price = 2134, SpentOn = ProductList[2]},
                new ProductExpenseDto{Type = "type6", Id = 6, Price = 2134, SpentOn = ProductList[3]},
                new ProductExpenseDto{Type = "type7", Id = 7, Price = 2134, SpentOn = ProductList[4]},
                new ProductExpenseDto{Type = "type8", Id = 8, Price = 2134, SpentOn = ProductList[5]},
                new ProductExpenseDto{Type = "type9", Id = 9, Price = 2134, SpentOn = ProductList[6]},
                new ProductExpenseDto{Type = "type10", Id = 10, Price = 2134, SpentOn = ProductList[7]},
                new ProductExpenseDto{Type = "type11", Id = 11, Price = 2134, SpentOn = ProductList[8]},
            };
            return View(productExpenses.Where(i => i.SpentOn.Id == Id));
        }

        public IActionResult Add(long Id)
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
            var expense = new ProductExpenseDto
            {
                ProductId = Id,
            };
            return View(expense);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(ProductExpenseDto productExpense)
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
            var productExpenses = new List<ProductExpenseDto>
            {
                new ProductExpenseDto{Type = "type1", Id = 1, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type2", Id = 2, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type3", Id = 3, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type4", Id = 4, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type5", Id = 5, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type6", Id = 6, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type7", Id = 7, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type8", Id = 8, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type9", Id = 9, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type10", Id = 9, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type11", Id = 9, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
            };

            return View(productExpenses.Where(i => i.Id == Id).First());
        }

        // GET: landProductExpense/Update landProductExpense
        public ActionResult Update(long Id)
        {
            var productExpenses = new List<ProductExpenseDto>
            {
                new ProductExpenseDto{Type = "type1", Id = 1, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type2", Id = 2, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type3", Id = 3, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type4", Id = 4, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type5", Id = 5, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type6", Id = 6, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type7", Id = 7, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type8", Id = 8, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type9", Id = 9, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type10", Id = 9, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type11", Id = 9, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
            };

            return View(productExpenses.Where(i => i.Id == Id).First());
        }

        // POST: landProductExpense/Update landProductExpense
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(ProductExpenseDto productExpense)
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

        // GET: landProductExpense/Delete landProductExpense
        public ActionResult Delete(long Id)
        {
            var productExpenses = new List<ProductExpenseDto>
            {
                new ProductExpenseDto{Type = "type1", Id = 1, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type2", Id = 2, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type3", Id = 3, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type4", Id = 4, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type5", Id = 5, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type6", Id = 6, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type7", Id = 7, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type8", Id = 8, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type9", Id = 9, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type10", Id = 9, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type11", Id = 9, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
            };

            return View(productExpenses.Where(i => i.Id == Id).First());
        }

        // POST: landProductExpense/Delete landProductExpense
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(ProductExpenseDto productExpense)
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

        // GET: landProductExpense/Get landProductExpense
        public ActionResult GetLandProductExpense(long Id)
        {
            var productExpenses = new List<ProductExpenseDto>
            {
                new ProductExpenseDto{Type = "type1", Id = 1, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type2", Id = 2, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type3", Id = 3, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type4", Id = 4, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type5", Id = 5, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type6", Id = 6, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type7", Id = 7, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type8", Id = 8, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type9", Id = 9, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type10", Id = 9, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type11", Id = 9, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
            };

            return View(productExpenses.Where(i => i.Id == Id).First());
        }
    }
}