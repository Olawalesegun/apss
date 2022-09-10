using Microsoft.AspNetCore.Mvc;
using APSS.Domain.Entities;
using APSS.Domain.Services;
using APSS.Web.Mvc.Auth;
using AutoMapper;
using APSS.Web.Dtos;
using APSS.Web.Mvc.Util.Navigation.Routes;
using APSS.Web.Dtos.Parameters;
using APSS.Web.Mvc.Models;

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

        [ApssAuthorized(AccessLevel.Root, PermissionType.Read)]
        public async Task<IActionResult> Index([FromQuery] FilteringParameters args)
        {
            var unitList = await _landSvc.GetLandProductUnitsAsync()
                .Where(u => u.Name.Contains(args.Query))
                .Page(args.Page, args.PageLength)
                .AsAsyncEnumerable()
                .Select(_mapper.Map<LandProductUnitDto>)
                .ToListAsync();

            return View(new CrudViewModel<LandProductUnitDto>(unitList, args));
        }

        // GET: LandProductUnitController/Add a new LandProductUnit
        [ApssAuthorized(AccessLevel.Root, PermissionType.Create)]
        public ActionResult Add()
        {
            return View();
        }

        // POST: LandProductUnitController/Add a new LandProductUnit
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ApssAuthorized(AccessLevel.Root, PermissionType.Create)]
        public async Task<IActionResult> Add(LandProductUnitDto landProductUnit)
        {
            if (!ModelState.IsValid)
            {
            }
            await _landSvc.AddLandProductUnitAsync(User.GetAccountId(), landProductUnit!.Name);
            TempData["success"] = "Unit added";

            return LocalRedirect(Routes.Dashboard.Lands.Units.FullPath);
        }

        // GET: LandProductUnitController/Update LandProductUnit
        [HttpGet]
        [ApssAuthorized(AccessLevel.Root, PermissionType.Update)]
        public async Task<IActionResult> Update(long Id)
        {
            return View(_mapper.Map<LandProductUnitDto>(
                await (await _landSvc.GetLandProductUnitAsync(User.GetAccountId(), Id)).FirstAsync()));
        }

        // POST: LandProductUnit/Update LandProductUnit
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ApssAuthorized(AccessLevel.Root, PermissionType.Update)]
        public async Task<IActionResult> Update(LandProductUnitDto landProductUnit)
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
            TempData["success"] = "Unit updated";

            return LocalRedirect(Routes.Dashboard.Lands.Units.FullPath);
        }

        // GET: LandProductUnitController/Delete LandProductUnit
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ApssAuthorized(AccessLevel.Root, PermissionType.Delete)]
        public async Task<IActionResult> Delete(long id)
        {
            await _landSvc.RemoveLandProductUnitAsync(User.GetAccountId(), id);
            TempData["success"] = "Unit removed";

            return LocalRedirect(Routes.Dashboard.Lands.Units.FullPath);
        }

        // POST: LandProductUnitController/Delete LandProductUnit
        //[ApssAuthorized(AccessLevel.Root, PermissionType.Delete)]
        /*public async Task<ActionResult> DeletePost(long Id)
        {
            await _landSvc.RemoveLandProductUnitAsync(User.GetAccountId(), Id);
            TempData["success"] = "Unit removed";

            return LocalRedirect(Routes.Dashboard.Lands.Units.FullPath);
        }
*/

        // GET: LandProductUnitController/Get LandProductUnit
        public async Task<IActionResult> GetLandProductUnit(long Id)
        {
            return View(_mapper.Map<LandProductUnitDto>(
                await (await _landSvc.GetLandProductUnitAsync(User.GetAccountId(), Id)).FirstAsync()));
        }
    }
}