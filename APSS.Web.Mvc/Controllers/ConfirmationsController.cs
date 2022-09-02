using Microsoft.AspNetCore.Mvc;
using APSS.Web.Dtos;

namespace APSS.Web.Mvc.Controllers
{
    public class ConfirmationsController : Controller
    {
        private List<AnimalGroupDto> animalgroup = new List<AnimalGroupDto>
            {
                new AnimalGroupDto{Id = 1, Type="type 1",Quantity=10,CreatedAt=new DateTime().AddHours(1)},
                new AnimalGroupDto{Id = 2, Type="type 1",Quantity=10,CreatedAt=new DateTime().AddHours(1)},
                new AnimalGroupDto{Id = 3, Type="type 1",Quantity=10,CreatedAt=new DateTime().AddHours(1)},
                new AnimalGroupDto{Id = 4, Type="type 1",Quantity=10,CreatedAt=new DateTime().AddHours(1)},
                new AnimalGroupDto{Id = 5, Type="type 1",Quantity=10,CreatedAt=new DateTime().AddHours(1)},
                new AnimalGroupDto{Id = 7, Type="type 1",Quantity=10,CreatedAt=new DateTime().AddHours(1)},
                new AnimalGroupDto{Id = 8,Type="type 1",Quantity=10,CreatedAt=new DateTime().AddHours(1)},
                new AnimalGroupDto{Id = 9, Type="type 1",Quantity=10,CreatedAt=new DateTime().AddHours(1)}
            };

        private List<AnimalProductDto> animalProduct = new List<AnimalProductDto>
            {
                new AnimalProductDto{Id=1,Name="name 1",Quantity=20,CreatedAt=new DateTime().AddHours(1)},
                new AnimalProductDto{Id=2,Name="name 1",Quantity=20,CreatedAt=new DateTime().AddHours(1)},
                new AnimalProductDto{Id=3,Name="name 1",Quantity=20,CreatedAt=new DateTime().AddHours(1)},
                new AnimalProductDto{Id=4,Name="name 1",Quantity=20,CreatedAt=new DateTime().AddHours(1)},
                new AnimalProductDto{Id=5,Name="name 1",Quantity=20,CreatedAt=new DateTime().AddHours(1)},
                new AnimalProductDto{Id=6,Name="name 1",Quantity=20,CreatedAt=new DateTime().AddHours(1)},
                new AnimalProductDto{Id=7,Name="name 1",Quantity=20,CreatedAt=new DateTime().AddHours(1)},
            };

        public ConfirmationsController()
        {
        }

        public async Task<IActionResult> Index()
        {
            var confirmation = new ConfirmationDto();
            confirmation.AnimalGroups = animalgroup;
            confirmation.AnimalProducts = animalProduct;
            return View(confirmation);
        }

        public async Task<IActionResult> ConfirmAnimalGroup(long id)
        {
            var confirm = animalgroup.Where(a => a.Id == id).FirstOrDefault();
            return View(confirm);
        }

        public async Task<IActionResult> ConfirmAnimal(long id, bool value)
        {
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ConfirmAnimalProduct(long id)
        {
            var confirm = animalProduct.Where(a => a.Id == id).FirstOrDefault();
            return View(confirm);
        }

        public async Task<IActionResult> ConfirmProduct(long id, bool value)
        {
            return RedirectToAction("Index");
        }
    }
}