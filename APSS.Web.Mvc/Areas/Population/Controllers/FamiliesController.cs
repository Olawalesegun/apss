using System.Linq;

using Microsoft.AspNetCore.Mvc;
using APSS.Application.App;
using APSS.Domain.Entities;
using APSS.Domain.Services;
using APSS.Web.Dtos;
using APSS.Web.Dtos.Forms;
using APSS.Web.Mvc.Auth;

using AutoMapper;
using APSS.Web.Dtos.Parameters;
using APSS.Web.Mvc.Models;

namespace APSS.Web.Mvc.Areas.Populatoin.Controllers
{
    [Area(Areas.Population)]
    public class FamiliesController : Controller
    {
        #region Fields

        private readonly IMapper _mapper;
        private readonly IPopulationService _populationSvc;

        #endregion Fields

        #region Public Constructors

        public FamiliesController(IPopulationService populationService, IMapper mapper)
        {
            _populationSvc = populationService;
            _mapper = mapper;
        }

        #endregion Public Constructors

        // GET: FamilyController/GetFamilies

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] FilteringParameters args)
        {
            var ret = await (await _populationSvc.GetFamilies(User.GetAccountId()))
                .Where(u => u.Name.Contains(args.Query))
                .Page(args.Page, args.PageLength)
                .AsAsyncEnumerable()
                .Select(_mapper.Map<FamilyDto>)
                .ToListAsync();

            return View(new CrudViewModel<FamilyDto>(ret, args));
        }

        // GET: FamilyController/FamilyDetails/5
        public async Task<IActionResult> Details(long id)
        {
            var family = await _populationSvc.GetFamilyAsync(User.GetAccountId(), id);
            var familyDto = new FamilyDto
            {
                Id = family.Id,
                Name = family.Name,
                LivingLocation = family.LivingLocation,
                UserName = family.AddedBy.Name,
                CreatedAt = family.CreatedAt,
                ModifiedAt = family.ModifiedAt,
            };
            return View(familyDto);
        }

        // GET: FamilyController/GetFamilyIndividuals/5
        public async Task<IActionResult> GetFamilyIndividuals(long id)
        {
            List<FamilyIndividualGetDto> familyindividualsDto = new List<FamilyIndividualGetDto>();
            var familyindividuals = await _populationSvc
                .GetIndividualsOfFamilyAsync(User.GetAccountId(), id);

            foreach (var familindividual in await familyindividuals.AsAsyncEnumerable().ToListAsync())
            {
                familyindividualsDto.Add(new FamilyIndividualGetDto
                {
                    Id = familindividual.Id,
                    NameFamily = familindividual.Family.Name,
                    NameIndividual = familindividual.Individual.Name,
                    IsParent = familindividual.IsParent,
                    IsProvider = familindividual.IsProvider,
                });
            }
            return View(familyindividualsDto);
        }

        // GET: FamilyController/AddFamily
        public IActionResult Add()
        {
            return View();
        }

        // POST: FamilyController/AddFamily
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([FromForm] FamilyAddForm family)
        {
            if (!ModelState.IsValid)
            {
                return View(family);
            }
            await _populationSvc.AddFamilyAsync(User.GetAccountId(), family.Name, family.LivingLocation);
            return RedirectToAction(nameof(Index));
        }

        // GET: FamilyController/EditFamily/5
        public async Task<IActionResult> Update(long id)
        {
            var family = await _populationSvc.GetFamilyAsync(User.GetAccountId(), id);
            var familyDto = new FamilyEditForm
            {
                Id = family.Id,
                Name = family.Name,
                LivingLocation = family.LivingLocation
            };

            return View( familyDto);
        }

        // POST: FamilyController/EditFamily/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(long id, [FromForm] FamilyAddForm family)
        {
            if (!ModelState.IsValid)
            {
                return View(family);
            }
            var familynew = await _populationSvc
                .UpdateFamilyAsync(User.GetAccountId(), id,
                f =>
                {
                    f.Name = family.Name;
                    f.LivingLocation = family.LivingLocation;
                });

            return RedirectToAction(nameof(Index));
        }

        // GET: FamilyController/DeleteFamily/5
        public async Task<IActionResult> Delete(long id)
        {
            var family = await _populationSvc.GetFamilyAsync(User.GetAccountId(), id);
            if (family == null)
            {
                return View();
            }
            var familydto = new FamilyDto
            {
                Id = family.Id,
                Name = family.Name,
                LivingLocation = family.LivingLocation,
                CreatedAt = family.CreatedAt,
                ModifiedAt = family.ModifiedAt,
                UserName = family.AddedBy.Name
            };
            return View(familydto);
        }

        // POST: FamilyController/DeleteFamily/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(long id, FamilyDto family)
        {
            if (id == family.Id)
                await _populationSvc.RemoveFamilyAsync(User.GetAccountId(), id);
            return RedirectToAction(nameof(Index));
        }
    }
}