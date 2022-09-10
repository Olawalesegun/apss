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
            var familyDto = _mapper.Map<FamilyDto>(family);
            return View(familyDto);
        }

        // GET: FamilyController/GetFamilyIndividuals/5
        public async Task<IActionResult> GetFamilyIndividuals(long id)
        {
            List<FamilyIndividualGetDto> familyindividualsDto = new List<FamilyIndividualGetDto>();
            var familyindividuals = await _populationSvc
                .GetIndividualsOfFamilyAsync(User.GetAccountId(), id);
            foreach (var familyindividual in await familyindividuals.AsAsyncEnumerable().ToListAsync())
            {
                familyindividualsDto.Add(new FamilyIndividualGetDto
                {
                    Id = familyindividual.Id,
                    NameFamily = familyindividual.Family.Name,
                    NameIndividual = familyindividual.Individual.Name,
                    CreatedAt = familyindividual.CreatedAt,
                    IsParent = familyindividual.IsParent,
                    IsProvider = familyindividual.IsProvider
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
            FamilyEditForm familyDto = new FamilyEditForm
            {
                Id = family.Id,
                Name = family.Name,
                LivingLocation = family.LivingLocation
            };

            return View(familyDto);
        }

        // POST: FamilyController/EditFamily/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromForm] FamilyEditForm familynew)
        {
            if (!ModelState.IsValid)
            {
                return View(familynew);
            }

            await _populationSvc
               .UpdateFamilyAsync(User.GetAccountId(), familynew.Id,
               f =>
               {
                   f.Name = familynew.Name;
                   f.LivingLocation = familynew.LivingLocation;
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
            var familydto = _mapper.Map<FamilyDto>(family);
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