using APSS.Domain.Services;
using APSS.Web.Dtos;
using APSS.Web.Mvc.Auth;
using Microsoft.AspNetCore.Mvc;

namespace APSS.Web.Mvc.Areas.Controllers
{
    [Area(Areas.Animals)]
    public class AnimalUnitsController : Controller
    {
        private readonly IAnimalService _aps;

        public IEnumerable<AnimalProductUnitDto> listUnit = new List<AnimalProductUnitDto>
        {
        };

        public AnimalUnitsController(IAnimalService aps)
        {
            _aps = aps;
        }

        public async Task<IActionResult> Index()
        {
            var unitDto = new List<AnimalProductUnitDto>();
            var units = await (await _aps.GetAnimalProductUnitAsync(User.GetId())).Where(i => i.Id > 0).AsAsyncEnumerable().ToListAsync();
            foreach (var unit in units)
            {
                unitDto.Add(new AnimalProductUnitDto
                {
                    Id = unit.Id,
                    Name = unit.Name,
                    CreatedAt = unit.CreatedAt,
                    ModifiedAt = unit.ModifiedAt
                });
            }

            return View(unitDto);
        }

        public async Task<IActionResult> AddUnit()
        {
            var productUnit = new AnimalProductUnitDto();
            return View(productUnit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUnit(AnimalProductUnitDto animalProductUnitDto)
        {
            if (ModelState.IsValid)
            {
                var add = await _aps.CreateAnimalProductUnitAsync(User.GetId(), animalProductUnitDto.Name);
                if (add == null) return RedirectToAction(nameof(Index));
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