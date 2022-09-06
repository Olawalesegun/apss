using APSS.Application.App;
using APSS.Domain.Entities;
using APSS.Domain.Services;
using APSS.Web.Dtos;
using APSS.Web.Dtos.Forms;
using APSS.Web.Mvc.Auth;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

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

        public async Task<IActionResult> Index()
        {
            var families = await _populationSvc.GetFamilies(User.GetId()).AsAsyncEnumerable().ToListAsync();
            List<FamilyDto> familiesdto = new List<FamilyDto>();
            foreach (var family in families)
            {
                familiesdto.Add(new FamilyDto
                {
                    Id = family.Id,
                    Name = family.Name,
                    LivingLocation = family.LivingLocation,
                    UserName = family.AddedBy.Name,
                    CreatedAt = family.CreatedAt,
                    ModifiedAt = family.ModifiedAt
                });
            }
            return View("GetFamilies", familiesdto);
        }

        // GET: FamilyController/FamilyDetails/5
        public async Task<IActionResult> FamilyDetails(long id)
        {
            var family = await _populationSvc.GetFamilyAsync(User.GetId(), id);
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
                .GetIndividualsOfFamilyAsync(User.GetId(), id);

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
        public IActionResult AddFamily()
        {
            return View();
        }

        // POST: FamilyController/AddFamily
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddFamily([FromForm] FamilyAddForm family)
        {
            if (!ModelState.IsValid)
            {
                return View(family);
            }
            await _populationSvc.AddFamilyAsync(User.GetId(), family.Name, family.LivingLocation);
            return RedirectToAction(nameof(Index));
        }

        // GET: FamilyController/EditFamily/5
        public async Task<IActionResult> UpdateFamily(long id)
        {
            var family = await _populationSvc.GetFamilyAsync(User.GetId(), id);
            var familyDto = new FamilyEditForm
            {
                Id = family.Id,
                Name = family.Name,
                LivingLocation = family.LivingLocation
            };

            return View("Editfamily", familyDto);
        }

        // POST: FamilyController/EditFamily/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateFamily(long id, [FromForm] FamilyAddForm family)
        {
            if (!ModelState.IsValid)
            {
                return View(family);
            }
            var familynew = await _populationSvc
                .UpdateFamilyAsync(User.GetId(), id,
                f =>
                {
                    f.Name = family.Name;
                    f.LivingLocation = family.LivingLocation;
                });

            return RedirectToAction(nameof(Index));
        }

        // GET: FamilyController/DeleteFamily/5
        public async Task<IActionResult> ConfirmDeleteFamily(long id)
        {
            var family = await _populationSvc.GetFamilyAsync(User.GetId(), id);
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
        [HttpPost, ActionName("DeleteFamily")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteFamily(long id, FamilyDto family)
        {
            if (id == family.Id)
                await _populationSvc.RemoveFamilyAsync(User.GetId(), id);
            return RedirectToAction(nameof(Index));
        }
    }
}