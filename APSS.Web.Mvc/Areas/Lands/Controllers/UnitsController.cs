using Microsoft.AspNetCore.Mvc;
using APSS.Domain.Entities;
using APSS.Domain.Services;
using APSS.Web.Mvc.Auth;
using AutoMapper;
using APSS.Web.Dtos;

namespace APSS.Web.Mvc.Areas.Lands.Controllers
{
    [Area(Areas.Lands)]
    public class UnitsController : Controller
    {
        private readonly ILandService _landSvc;
        private readonly IMapper _mapper;
        private readonly List<LandProductUnitDto> _unitsList;

        public UnitsController(ILandService landService, IMapper mapper)
        {
            _landSvc = landService;
            _mapper = mapper;
            _unitsList = new List<LandProductUnitDto>();
        }

        [ApssAuthorized(AccessLevel.Root, PermissionType.Read)]
        public async Task<IActionResult> Index()
        {
            var unitList = await _landSvc.
                GetLandProductUnitsAsync()
                .AsAsyncEnumerable()
                .ToListAsync();
            foreach (var unit in unitList)
            {
                var item = _mapper.Map<LandProductUnitDto>(unit);
                _unitsList.Add(item);
            }

            return View(_unitsList);
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
        public async Task<ActionResult> Add(LandProductUnitDto landProductUnit)
        {
            if (!ModelState.IsValid || landProductUnit == null)
            {
            }
            await _landSvc.AddLandProductUnitAsync(User.GetId(), landProductUnit!.Name);

            return RedirectToAction("Index");
        }

        // GET: LandProductUnitController/Update LandProductUnit
        [ApssAuthorized(AccessLevel.Root, PermissionType.Update)]
        public async Task<ActionResult> Update(long Id)
        {
            return View(_mapper.Map<LandProductUnitDto>(
                await _landSvc.GetLandProductUnitAsync(User.GetId(), Id)));
        }

        // POST: LandProductUnit/Update LandProductUnit
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ApssAuthorized(AccessLevel.Root, PermissionType.Update)]
        public async Task<ActionResult> Update(LandProductUnitDto landProductUnit)
        {
            if (!ModelState.IsValid || landProductUnit == null)
            {
            }
            await _landSvc.UpdateLandProductUnitAsync(User.GetId(),
                landProductUnit!.Id,
                f =>
                {
                    f.Name = landProductUnit.Name;
                });

            return RedirectToAction("Index");
        }

        // GET: LandProductUnitController/Delete LandProductUnit
        [ApssAuthorized(AccessLevel.Root, PermissionType.Delete)]
        public async Task<ActionResult> Delete(long Id)
        {
            if (Id <= 0)
            { }

            return View(_mapper.Map<LandProductUnitDto>(
                await _landSvc.GetLandProductUnitAsync(User.GetId(), Id)));
        }

        // POST: LandProductUnitController/Delete LandProductUnit
        [HttpPost]
        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeletePost(long Id)
        {
            if (Id <= 0)
            { }
            await _landSvc.RemoveLandProductUnitAsync(User.GetId(), Id);

            return RedirectToAction("Index");
        }

        // GET: LandProductUnitController/Get LandProductUnit
        public async Task<ActionResult> GetLandProductUnit(long Id)
        {
            if (Id <= 0)
            { }
            return View(_mapper.Map<LandProductUnitDto>(
                await _landSvc.GetLandProductUnitAsync(User.GetId(), Id)));
        }
    }
}