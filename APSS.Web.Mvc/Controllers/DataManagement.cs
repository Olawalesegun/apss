using APSS.Domain.Entities;
using APSS.Domain.Repositories;
using APSS.Domain.Services;
using APSS.Web.Dtos;
using APSS.Web.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace APSS.Web.Mvc.Controllers
{
    public class DataManagement : Controller
    {
        private IEnumerable<AnimalGroupDto> animal;

        public DataManagement()
        {
            animal = new List<AnimalGroupDto>
            {
                new AnimalGroupDto{Id=1,Type="type 1",Quantity=100,CreatedAt=new DateTime(),Sex=Domain.Entities.AnimalSex.Female},
                new AnimalGroupDto{Id=2,Type="type 2",Quantity=100,CreatedAt=new DateTime(),Sex=Domain.Entities.AnimalSex.Female},
                new AnimalGroupDto{Id=3,Type="type 3",Quantity=100,CreatedAt=new DateTime(),Sex=Domain.Entities.AnimalSex.Female},
                new AnimalGroupDto{Id=4,Type="type 4",Quantity=100,CreatedAt=new DateTime(),Sex=Domain.Entities.AnimalSex.Female},
                new AnimalGroupDto{Id=5,Type="type 5",Quantity=100,CreatedAt=new DateTime(),Sex=Domain.Entities.AnimalSex.Female},
            };
        }

        public IActionResult Index()
        {
            var total = new AnimalGroupAndProductDto
            {
                AnimalGroupDtos = animal.ToList(),
                AnimalProducts = new AnimalProductDto(),
            };
            return View(total);
        }

        public async Task<IActionResult> AnimalProduct()
        {
            return View();
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

            return View(animalProductDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAnimalProduct(AnimalProductDto animalProductDto)
        {
            if (ModelState.IsValid)
            {
                return View(animalProductDto);
            }
            return View();
        }

        public async Task<IActionResult> AnimalDetails(int id)
        {
            AnimalGroupAndProductDto animalGroupDto = new AnimalGroupAndProductDto();

            return View(animalGroupDto);
        }

        public async Task<IActionResult> AnimalProductdetails(int Id)
        {
            AnimalProductDto animalProductDto = new AnimalProductDto();

            return View(animalProductDto);
        }

        [HttpGet]
        public async Task<IActionResult> AddAnimalGroup(AnimalGroupDto animal)
        {
            return View();
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

        public async Task<IActionResult> EditAnimalProduct(int id)
        {
            AnimalProductDto animalProductDto = new AnimalProductDto();
            return View(animalProductDto);
        }

        [HttpPost]
        public async Task<IActionResult> EditAnimalProduct(AnimalProductDto productObj)
        {
            if (ModelState.IsValid)
            {
                RedirectToAction("AnimalProduct");
            }

            return View(productObj);
        }

        public async Task<IActionResult> DeleteAnimalProduct(int id)
        {
            AnimalProductDto animalProductDto = new AnimalProductDto();
            return View(animalProductDto);
        }

        public async Task<IActionResult> ConfirmDeleteAnimalProduct(int id)
        {
            AnimalProductDto animalProductDto = new AnimalProductDto();
            if (id != null)
            {
                RedirectToAction("AnimalProduct");
            }

            return View("AnimalProduct");
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
            return View(expense);
        }

        public async Task<IActionResult> DeleteAnimalGroup(int id)
        {
            AnimalGroupDto animalGroupDto = new AnimalGroupDto();
            return View(animalGroupDto);
        }

        public async Task<IActionResult> ConfirmDeleteAnimalGroup(int id)
        {
            AnimalGroupDto animalGroupDto = new AnimalGroupDto();
            if (true)
            {
                RedirectToAction("Index");
            }
            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Search()
        {
            return View(animal);
        }
    }
}