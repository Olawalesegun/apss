using Microsoft.AspNetCore.Mvc;
using APSS.Web.Dtos;

namespace APSS.Web.Mvc.Areas.Controllers
{
    [Area(Areas.Animals)]
    public class ProductsController : Controller
    {
        private IEnumerable<AnimalProductDto> product;

        public ProductsController()
        {
            product = new List<AnimalProductDto>
            {
                new AnimalProductDto{Id=1, Name ="product 1",Quantity=10,CreatedAt=DateTime.Now,ModifiedAt=DateTime.Now},
                new AnimalProductDto{Id=2, Name ="one 1",Quantity=13,CreatedAt=DateTime.Now,ModifiedAt=DateTime.Now},
                new AnimalProductDto{Id=3, Name ="two 1",Quantity=18,CreatedAt=DateTime.Now,ModifiedAt=DateTime.Now},
                new AnimalProductDto{Id=4, Name ="one 1",Quantity=16,CreatedAt=DateTime.Now,ModifiedAt=DateTime.Now},
                new AnimalProductDto{Id=5, Name ="product 2",Quantity=33,CreatedAt=DateTime.Now,ModifiedAt=DateTime.Now},
                new AnimalProductDto{Id=6, Name ="product 3",Quantity=155,CreatedAt=DateTime.Now,ModifiedAt=DateTime.Now},
                new AnimalProductDto{Id=7, Name ="product 5",Quantity=177,CreatedAt=DateTime.Now,ModifiedAt=DateTime.Now},
                new AnimalProductDto{Id=8, Name ="product 0",Quantity=100,CreatedAt=DateTime.Now,ModifiedAt=DateTime.Now},
            };
        }

        public IActionResult Index()
        {
            var animalProduct = product;
            return View(animalProduct);
        }

        public async Task<IActionResult> Add(int id)
        {
            List<AnimalProductUnitDto> animalProductUnit = new List<AnimalProductUnitDto>
            {
                new AnimalProductUnitDto{Id=1,Name="كيلو"},
                new AnimalProductUnitDto{Id=3,Name="طن"},
                new AnimalProductUnitDto{Id=5,Name="جرام"},
                new AnimalProductUnitDto{Id=6,Name="1كيلو"},
            };
            ViewBag.units = animalProductUnit;
            var animalProductDto = new AnimalProductDto
            {
                ProducerId = id,
            };

            return View(animalProductDto);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string searchString, string searchBy)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.Trim();
                if (searchBy == "2")
                {
                    List<AnimalProductDto> response = new List<AnimalProductDto>();
                    response = product.Where(p => p.Id == Convert.ToInt32(searchString)).ToList();
                    return View(response);
                }
                else if (searchBy == "2")
                {
                    List<AnimalProductDto> response = new List<AnimalProductDto>();
                    response = product.Where(p => p.Quantity == Convert.ToInt32(searchString)).ToList();
                    return View(response);
                }
                else
                {
                    List<AnimalProductDto> response = new List<AnimalProductDto>();
                    response = product.Where(p => p.Name.Contains(searchString)).ToList();
                    return View(response);
                }
            }

            ViewBag.SearchResult = "ليس هناك نتائج عن " + searchString;

            var result = new List<AnimalProductDto>();
            return View(result);
        }

        public async Task<IActionResult> Details(int Id)
        {
            AnimalProductDto animalProductDto = new AnimalProductDto();

            return View(animalProductDto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            AnimalProductDto animalProductDto = new AnimalProductDto();
            return View(animalProductDto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AnimalProductDto productObj)
        {
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            AnimalProductDto animalProductDto = new AnimalProductDto();
            return View(animalProductDto);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            AnimalProductDto animalProductDto = new AnimalProductDto();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AddProductExpense(int id)
        {
            ProductExpenseDto productExpenseDto = new ProductExpenseDto();
            return View(productExpenseDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddProductExpense(ProductExpenseDto expense)
        {
            if (ModelState.IsValid)
            {
                return View(expense);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AllProducts()
        {
            AnimalGroupAndProductDto animalGroupProductDto = new AnimalGroupAndProductDto();
            AnimalProductDto animalproducts = new AnimalProductDto();
            animalGroupProductDto.AnimalProducts = animalproducts;

            return View(animalGroupProductDto);
        }

        public async Task<IActionResult> ListProductExpense(long id)
        {
            var expense = new List<ProductExpenseDto>();
            expense.Add(new ProductExpenseDto { Id = 1, Type = "شراء", Price = 10000 });
            return View(expense);
        }

        public async Task<IActionResult> EditProductExpense(long id)
        {
            var expense = new ProductExpenseDto();
            return View(expense);
        }

        [HttpPost]
        public async Task<IActionResult> EditProductExpense(ProductExpenseDto expense)
        {
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteProductExpense(int id)
        {
            var expense = new ProductExpenseDto();
            return View(expense);
        }

        public async Task<IActionResult> ConfirmDeleteProductExpense(ProductExpenseDto expense)
        {
            return RedirectToAction("Index");
        }
    }
}