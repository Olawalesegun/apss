using Microsoft.AspNetCore.Mvc;
using APSS.Domain.Entities;
using APSS.Domain.Services;
using APSS.Web.Mvc.Auth;
using AutoMapper;
using APSS.Web.Dtos;
using APSS.Web.Mvc.Util.Navigation.Routes;

namespace APSS.Web.Mvc.Areas.Lands.Controllers
{
    [Area(Areas.Lands)]
    public class UnitsController : Controller
    {
        private readonly ILandService _landSvc;
        private readonly IMapper _mapper;

        public UnitsController(ILandService landService, IMapper mapper)
        {
            _landSvc = landService;
            _mapper = mapper;
        }

        //[ApssAuthorized(AccessLevel.Root, PermissionType.Read)]
        public async Task<IActionResult> Index()
        {
            var unitList = await _landSvc.
                GetLandProductUnitsAsync()
                .AsAsyncEnumerable()
                .ToListAsync();

            return View(unitList.Select(_mapper.Map<LandProductUnitDto>));
        }

        // GET: LandProductUnitController/Add a new LandProductUnit
        //[ApssAuthorized(AccessLevel.Root, PermissionType.Create)]
        public ActionResult Add()
        {
            return View();
        }

        // POST: LandProductUnitController/Add a new LandProductUnit
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[ApssAuthorized(AccessLevel.Root, PermissionType.Create)]
        public async Task<ActionResult> Add(LandProductUnitDto landProductUnit)
        {
            if (!ModelState.IsValid)
            {
            }
            await _landSvc.AddLandProductUnitAsync(User.GetAccountId(), landProductUnit!.Name);

            return LocalRedirect(Routes.Dashboard.Lands.Units.FullPath);
        }

        // GET: LandProductUnitController/Update LandProductUnit
        [HttpGet]
        //[ApssAuthorized(AccessLevel.Root, PermissionType.Update)]
        public async Task<ActionResult> Update(long Id)
        {
            return View(_mapper.Map<LandProductUnitDto>(
                await (await _landSvc.GetLandProductUnitAsync(User.GetAccountId(), Id)).FirstAsync()));
        }

        // POST: LandProductUnit/Update LandProductUnit
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[ApssAuthorized(AccessLevel.Root, PermissionType.Update)]
        public async Task<ActionResult> Update(LandProductUnitDto landProductUnit)
        {
            if (!ModelState.IsValid)
            {
            }
            await _landSvc.UpdateLandProductUnitAsync(User.GetAccountId(),
                landProductUnit!.Id,
                f =>
                {
                    f.Name = landProductUnit.Name;
                });

            return LocalRedirect(Routes.Dashboard.Lands.Units.FullPath);
        }

        // GET: LandProductUnitController/Delete LandProductUnit
        //[ApssAuthorized(AccessLevel.Root, PermissionType.Delete)]
        public async Task<ActionResult> Delete(long Id)
        {
            return View(_mapper.Map<LandProductUnitDto>(
                await (await _landSvc.GetLandProductUnitAsync(User.GetAccountId(), Id)).FirstAsync()));
        }

        // POST: LandProductUnitController/Delete LandProductUnit
        //[ApssAuthorized(AccessLevel.Root, PermissionType.Delete)]
        public async Task<ActionResult> DeletePost(long Id)
        {
            await _landSvc.RemoveLandProductUnitAsync(User.GetAccountId(), Id);

            return LocalRedirect(Routes.Dashboard.Lands.Units.FullPath);
        }

        // GET: LandProductUnitController/Get LandProductUnit
        public async Task<ActionResult> GetLandProductUnit(long Id)
        {
            return View(_mapper.Map<LandProductUnitDto>(
                await (await _landSvc.GetLandProductUnitAsync(User.GetAccountId(), Id)).FirstAsync()));
        }
    }
}