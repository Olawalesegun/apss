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
        [ApssAuthorized(AccessLevel.All ^ AccessLevel.Farmer, PermissionType.Read)]
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
        [ApssAuthorized(AccessLevel.All ^ AccessLevel.Farmer, PermissionType.Read)]
        public async Task<IActionResult> Details(long id)
        {
            var family = await _populationSvc.GetFamilyAsync(User.GetAccountId(), id);
            var familyDto = _mapper.Map<FamilyDto>(family);
            return View(familyDto);
        }

        // GET: FamilyController/GetFamilyIndividuals/5
        [ApssAuthorized(AccessLevel.All ^ AccessLevel.Farmer, PermissionType.Read)]
        [HttpGet, ActionName("GetAll")]
        public async Task<IActionResult> GetAll(long id)
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
        [ApssAuthorized(AccessLevel.Group, PermissionType.Create)]
        public IActionResult Add()
        {
            return View();
        }

        // POST: FamilyController/AddFamily
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ApssAuthorized(AccessLevel.Group, PermissionType.Create)]
        public async Task<IActionResult> Add([FromForm] FamilyAddForm family)
        {
            if (!ModelState.IsValid)
            {
                return View(family);
            }
            await _populationSvc.AddFamilyAsync(User.GetAccountId(), family.Name, family.LivingLocation);
            TempData["success"] = "Family Added successfully";

            return RedirectToAction(nameof(Index));
        }

        // GET: FamilyController/EditFamily/5
        [ApssAuthorized(AccessLevel.Group, PermissionType.Update)]
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
        [ApssAuthorized(AccessLevel.Group, PermissionType.Update)]
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
            TempData["success"] = "Family Updated successfully";

            return RedirectToAction(nameof(Index));
        }

        // GET: FamilyController/DeleteFamily/5
        [ApssAuthorized(AccessLevel.Group, PermissionType.Delete)]
        public IActionResult Delete(long id)
        {
            return RedirectToAction(nameof(Index));
        }

        // POST: FamilyController/DeleteFamily/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(long id, FamilyDto family)
        {
            if (id == family.Id)
                await _populationSvc.RemoveFamilyAsync(User.GetAccountId(), id);
            TempData["success"] = "Family deleted successfully";

            return RedirectToAction(nameof(Index));
        }
    }
}