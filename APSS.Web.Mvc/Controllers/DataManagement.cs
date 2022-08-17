using APSS.Domain.Repositories;
using APSS.Domain.Services;
using APSS.Web.Dtos;
using APSS.Web.Mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace APSS.Web.Mvc.Controllers
{
    public class DataManagement : Controller
    {
        private List<AnimalGroupDto> animal;

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

        public async Task<IActionResult> CreateAnimalProduct()
        {
            return View();
        }

        public async Task<IActionResult> AnimalDetails()
        {
            return View();
        }

        public async Task<IActionResult> AnimalProductdetails(int Id)
        {
            return View();
        }

        /* public Task<IActionResult> AddAnimalGroup(AnimalGroupDto animal)
         {
             return View();
         }*/

        [HttpPost]
        public async Task<IActionResult> Search()
        {
            AnimalGroup animal = new AnimalGroup();
            animal = new AnimalGroup()
            {
                Id = 1,
                Type = "dog",
                Quantity = 100,
                Sex = "male"
            };
            return View(animal);
        }
    }
}