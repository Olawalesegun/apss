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
        private readonly UserDto _usereDto;
        private readonly List<FamilyDto> fes;
        private readonly List<IndividualDto> individuals;
        private readonly List<FamilyIndividualDto> familyIndividuals;

        #endregion Fields

        #region Public Constructors

        public FamiliesController(IPopulationService populationService, IMapper mapper)
        {
            _populationSvc = populationService;
            _mapper = mapper;
            _usereDto = new UserDto
            {
                Id = 1,
                Name = "aden",
                AccessLevel = AccessLevel.Root,
                userStatus = UserStatus.Active,
            };

            fes = new List<FamilyDto>
            {
              new FamilyDto{Id=54,Name="ali",LivingLocation="sana'a",CreatedAt=DateTime.Now,ModifiedAt=DateTime.Now,User=_usereDto },
              new FamilyDto{Id=53,Name="salih",LivingLocation="sana'a",CreatedAt=DateTime.Now,ModifiedAt=DateTime.Now,User=_usereDto },
            };

            individuals = new List<IndividualDto>
            {
                new IndividualDto{Id=54, Name="ali",Address="mareb",Family=fes.First(),User=_usereDto},
                new IndividualDto{Id=534, Name="salih",Address="mareb",Family=fes.Last(),User=_usereDto}
            };

            familyIndividuals = new List<FamilyIndividualDto>
            {
                new FamilyIndividualDto{Id=54,Individual=individuals.First(),Family=fes.First(),IsParent=true,IsProvider=true},
                new FamilyIndividualDto{Id=54,Individual=individuals.Last(),Family=fes.Last(),IsParent=true,IsProvider=true},
            };
        }

        #endregion Public Constructors

        // GET: FamilyController/GetFamilies

        public IActionResult Index()
        {
            /*var families = _populationSvc.GetFamilies(User.GetId());
            var familiesDto = _mapper.Map<FamilyDto>(families);
            *//*          var familiesDto= await families.AsAsyncEnumerable().Select(
                            f => new FamilyGetDto
                            {
                                Id = f.Id,
                                Name = f.Name,
                                LivingLocation = f.LivingLocation,
                                UserName=f.AddedBy.Name
                            }).ToListAsync();*/

            return View("GetFamilies", fes);
        }

        // GET: FamilyController/FamilyDetails/5
        [ApssAuthorized(AccessLevel.Root | AccessLevel.Presedint | AccessLevel.Directorate | AccessLevel.District
                       | AccessLevel.Village | AccessLevel.Governorate | AccessLevel.Group, PermissionType.Read)]
        public IActionResult FamilyDetails(long id)
        {
            var family = _populationSvc.GetFamilies(User.GetAccountId()).Where(f => f.Id == id);
            var familyDto = _mapper.Map<FamilyDto>(family);
            /* var familyDto= await  family.Where(f => f.Id == id)
                    .AsAsyncEnumerable()
                    .Select(f => new FamilyGetDto
                {
                    Id = f.Id,
                    Name = f.Name,
                    LivingLocation = f.LivingLocation,
                    CreatedAt = f.CreatedAt,
                    ModifiedAt = f.ModifiedAt
                }).ToListAsync();*/

            return View(familyDto);
        }

        // GET: FamilyController/GetFamilyIndividuals/5
        [ApssAuthorized(AccessLevel.Root | AccessLevel.Presedint | AccessLevel.Directorate | AccessLevel.District
                      | AccessLevel.Village | AccessLevel.Governorate | AccessLevel.Group, PermissionType.Read)]
        public async Task<IActionResult> GetFamilyIndividuals(long id)
        {
            var familyindividualsDto = await _populationSvc
                .GetIndividualsOfFamilyAsync(User.GetAccountId(), id);

            var indiviudalofFamilyDtoMap = _mapper.Map<FamilyIndividualDto>(familyindividualsDto);

            var individualoffamily = familyIndividuals.Where(f => f.Family.Id == id);
            return View(indiviudalofFamilyDtoMap);
        }

        // GET: FamilyController/AddFamily
        [ApssAuthorized(AccessLevel.Group, PermissionType.Create)]
        public IActionResult AddFamily()
        {
            return View();
        }

        // POST: FamilyController/AddFamily
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ApssAuthorized(AccessLevel.Group, PermissionType.Create)]
        public IActionResult AddFamily([FromForm] FamilyAddForm family)
        {
            if (!ModelState.IsValid)
            {
                return View(family);
            }
            _populationSvc.AddFamilyAsync(User.GetAccountId(), family.Name, family.LivingLocation);

            return RedirectToAction(nameof(family));
        }

        // GET: FamilyController/EditFamily/5
        [ApssAuthorized(AccessLevel.Group, PermissionType.Update)]
        public async Task<IActionResult> UpdateFamily(long id)
        {
            var family = await _populationSvc.GetFamilyAsync(User.GetAccountId(), id);
            var familyDto = _mapper.Map<FamilyDto>(family);

            return View("Editfamily", familyDto);
        }

        // POST: FamilyController/EditFamily/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ApssAuthorized(AccessLevel.Group, PermissionType.Update)]
        public async Task<IActionResult> UpdateFamily(long id, [FromForm] FamilyAddForm family)
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
        [ApssAuthorized(AccessLevel.Group, PermissionType.Delete)]
        public async Task<IActionResult> DeleteFamily(long id)
        {
            var family = await _populationSvc.GetFamilyAsync(User.GetAccountId(), id);
            if (family == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View("ConfirmDeleteFamily", family);
        }

        // POST: FamilyController/DeleteFamily/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ApssAuthorized(AccessLevel.Group, PermissionType.Delete)]
        public async Task<IActionResult> DeleteFamily(long id, FamilyDto family)
        {
            var Oldfamily = await _populationSvc.GetFamilyAsync(User.GetAccountId(), id);
            if (family == null | Oldfamily.Id != family!.Id)
            {
                return View("ConfirmDeleteFamily", family);
            }
            await _populationSvc.RemoveFamilyAsync(User.GetAccountId(), id);
            return RedirectToAction(nameof(Index));
        }
    }
}