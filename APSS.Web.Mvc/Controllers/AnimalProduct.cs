using APSS.Web.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace APSS.Web.Mvc.Controllers
{
    public class AnimalProduct : Controller
    {
        public IActionResult Index()
        {
            var animalProduct = new List<AnimalProductDto>();
            animalProduct.Add(new AnimalProductDto { Name = "product", Id = 1, Quantity = 10, CreatedAt = new DateTime() });
            return View(animalProduct);
        }

        public async Task<IActionResult> CreateAnimalProduct(int id)
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

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AnimalProductdetails(int Id)
        {
            AnimalProductDto animalProductDto = new AnimalProductDto();

            return View(animalProductDto);
        }

        public async Task<IActionResult> EditAnimalProduct(int id)
        {
            AnimalProductDto animalProductDto = new AnimalProductDto();
            return View(animalProductDto);
        }

        [HttpPost]
        public async Task<IActionResult> EditAnimalProduct(AnimalProductDto productObj)
        {
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteAnimalProduct(int id)
        {
            AnimalProductDto animalProductDto = new AnimalProductDto();
            return View(animalProductDto);
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmDeleteAnimalProduct(int id)
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

        public async Task<IActionResult> AllAnimalProducts()
        {
            AnimalGroupAndProductDto animalGroupProductDto = new AnimalGroupAndProductDto();
            AnimalProductDto animalproducts = new AnimalProductDto();
            animalGroupProductDto.AnimalProducts = animalproducts;
            if (!true)
            {
                RedirectToAction("AnimalDetails");
            }
            return View(animalGroupProductDto);
        }
    }
}