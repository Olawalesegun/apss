using APSS.Domain.Services;
using APSS.Web.Dtos;
using APSS.Web.Dtos.Parameters;
using APSS.Web.Mvc.Auth;
using APSS.Web.Mvc.Models;
using APSS.Web.Mvc.Util.Navigation.Routes;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace APSS.Web.Mvc.Areas.Animals.Controllers
{
    [Area(Areas.Animals)]
    public class
        UnitsController : Controller
    {
        private readonly IAnimalService _aps;
        private readonly IMapper _mapper;

        public UnitsController(IAnimalService aps, IMapper mapper)
        {
            _aps = aps;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index([FromQuery] FilteringParameters args)
        {
            //var unitDto = new List<AnimalProductUnitDto>();
            var units = await (await _aps.GetAnimalProductUnitAsync(User.GetAccountId()))
                 .Where(u => u.Name.Contains(args.Query))
            .Page(args.Page, args.PageLength)
            .AsAsyncEnumerable()
            .Select(_mapper.Map<AnimalProductUnitDto>)
            .AsAsyncEnumerable()
            .ToListAsync();
            /*foreach (var unit in units)
            {
                unitDto.Add(new AnimalProductUnitDto
                {
                    Id = unit.Id,
                    Name = unit.Name,
                    CreatedAt = unit.CreatedAt,
                    ModifiedAt = unit.ModifiedAt
                });
            }*/

            return View(new CrudViewModel<AnimalProductUnitDto>(units, args));
        }

        public IActionResult Add()
        {
            var productUnit = new AnimalProductUnitDto();
            return View(productUnit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AnimalProductUnitDto animalProductUnitDto)
        {
            if (ModelState.IsValid)
            {
                var add = await _aps.CreateAnimalProductUnitAsync(User.GetAccountId(), animalProductUnitDto.Name);
                if (add == null) return RedirectToAction(nameof(Index));
                return LocalRedirect(Routes.Dashboard.Animals.Units.FullPath);
            }

            return View(animalProductUnitDto);
        }

        public async Task<IActionResult> Update(long id)
        {
            var units = new AnimalProductUnitDto();
            var animalUnits = await (await _aps.GetAnimalProductUnitAsync(User.GetAccountId()))
                .Where(i => i.Id == id)
                .AsAsyncEnumerable()
                .FirstAsync();
            units.Name = animalUnits.Name;
            units.Id = animalUnits.Id;
            return View(units);
        }

        [HttpPost]
        public async Task<IActionResult> Update(AnimalProductUnitDto animalProductUnitDto)
        {
            if (!ModelState.IsValid) return View(animalProductUnitDto);
            var update = await _aps.UpdateProductUnit(User.GetAccountId(),
                animalProductUnitDto.Id,
                u => u.Name = animalProductUnitDto.Name);
            return LocalRedirect(Routes.Dashboard.Animals.Units.FullPath);
        }

        public async Task<IActionResult> Delete(long id)
        {
            var units = new AnimalProductUnitDto();
            var animalUnits = await (await _aps.GetAnimalProductUnitAsync(User.GetAccountId()))
                .Where(i => i.Id == id)
                .AsAsyncEnumerable()
                .FirstAsync();
            units.Name = animalUnits.Name;
            units.Id = animalUnits.Id;
            return View(units);
        }

        public async Task<IActionResult> ConfirmDelete(long id)
        {
            await _aps.RemoveAnimalProductUnitAsync(User.GetAccountId(), id);
            return LocalRedirect(Routes.Dashboard.Animals.Units.FullPath);
        }
    }
}