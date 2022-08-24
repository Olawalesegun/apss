using APSS.Domain.Entities;
using APSS.Domain.Repositories;
using APSS.Domain.Services;
using APSS.Web.Dtos;
using APSS.Web.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace APSS.Web.Mvc.Controllers
{
    public class AnimalGroup : Controller
    {
        private IEnumerable<AnimalGroupDto> animal;

        public AnimalGroup()
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
            var total = new AnimalGroupAndProductDto
            {
                AnimalProductList = new List<AnimalProductDto>(),
            };
            return View(total);
        }

        public async Task<IActionResult> AddUnit(long Id)
        {
            var animalUnit = new AnimalProductUnitDto();

            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddUnit(AnimalProductUnitDto unit)
        {
            var animalUnit = new AnimalProductUnitDto();
            if (ModelState.IsValid)
            {
                BadRequest("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> CreateAnimalProduct(AnimalProductDto animalProductDto)
        {
            if (ModelState.IsValid)
            {
                return View(animalProductDto);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AnimalDetails(int id)
        {
            AnimalGroupAndProductDto animalGroupDto = new AnimalGroupAndProductDto();

            return View(animalGroupDto);
        }

        [HttpGet]
        public async Task<IActionResult> AddAnimalGroup(AnimalGroupDto animal)
        {
            return View();
        }

        public async Task<IActionResult> DeleteAnimalGroup(int id)
        {
            AnimalGroupDto animalGroupDto = new AnimalGroupDto();
            return View(animalGroupDto);
        }

        public async Task<IActionResult> ConfirmDeleteAnimalGroup(int id)
        {
            AnimalGroupDto animalGroupDto = new AnimalGroupDto();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EditAnimalGroup(int id)
        {
            AnimalGroupDto animalGroupDto = new AnimalGroupDto();
            return View(animalGroupDto);
        }

        [HttpPost]
        public async Task<IActionResult> EditAnimalGroup(AnimalGroupDto animalGroupDto)
        {
            return RedirectToAction("Index");
        }
    }
}