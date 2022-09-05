using APSS.Web.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace APSS.Web.Mvc.Controllers
{
    [Area(Areas.Areas.Animals)]
    public class AnimalUnitsController : Controller
    {
        public IEnumerable<AnimalProductUnitDto> listUnit = new List<AnimalProductUnitDto>
         {
                new AnimalProductUnitDto{Id=1,Name="unit 1",CreatedAt=DateTime.Now,ModifiedAt=DateTime.Today},
                new AnimalProductUnitDto{Id=2,Name="unit 2",CreatedAt=DateTime.Now,ModifiedAt=DateTime.Today},
                new AnimalProductUnitDto{Id=3,Name="unit 3",CreatedAt=DateTime.Now,ModifiedAt=DateTime.Today},
                new AnimalProductUnitDto{Id=4,Name="unit 4",CreatedAt=DateTime.Now,ModifiedAt=DateTime.Today},
                new AnimalProductUnitDto{Id=5,Name="unit 5",CreatedAt=DateTime.Now,ModifiedAt=DateTime.Today},
                new AnimalProductUnitDto{Id=6,Name="unit 6",CreatedAt=DateTime.Now,ModifiedAt=DateTime.Today},
                new AnimalProductUnitDto{Id=7,Name="unit 7",CreatedAt=DateTime.Now,ModifiedAt=DateTime.Today},
                new AnimalProductUnitDto{Id=8,Name="unit 8",CreatedAt=DateTime.Now,ModifiedAt=DateTime.Today},
                new AnimalProductUnitDto{Id=8,Name="unit 9",CreatedAt=DateTime.Now,ModifiedAt=DateTime.Today},
                new AnimalProductUnitDto{Id=8,Name="unit 10",CreatedAt=DateTime.Now,ModifiedAt=DateTime.Today},
                new AnimalProductUnitDto{Id=8,Name="unit 11",CreatedAt=DateTime.Now,ModifiedAt=DateTime.Today},
                new AnimalProductUnitDto{Id=8,Name="unit 12",CreatedAt=DateTime.Now,ModifiedAt=DateTime.Today},
                new AnimalProductUnitDto{Id=8,Name="unit 13",CreatedAt=DateTime.Now,ModifiedAt=DateTime.Today},
            };

        public IActionResult Index()
        {
            var unit = listUnit;

            return View(unit);
        }

        public async Task<IActionResult> AddUnit()
        {
            var productUnit = new AnimalProductUnitDto();
            return View(productUnit);
        }

        [HttpPost]
        public async Task<IActionResult> AddUnit(AnimalProductUnitDto animalProductUnitDto)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            return View(animalProductUnitDto);
        }

        public async Task<IActionResult> EditUnit(long id)
        {
            var uints = new AnimalProductUnitDto();
            return View(uints);
        }

        [HttpPost]
        public async Task<IActionResult> EditUnit(AnimalProductUnitDto animalProductUnitDto)
        {
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteUnit(long id)
        {
            var units = new AnimalProductUnitDto();
            units = listUnit.Where(u => u.Id == id).FirstOrDefault();

            return View(units);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmDeletUhnit(long id)
        {
            return RedirectToAction("Index");
        }
    }
}