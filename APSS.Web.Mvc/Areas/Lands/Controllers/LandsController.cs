using Microsoft.AspNetCore.Mvc;
using APSS.Domain.Entities;
using APSS.Domain.Services;
using APSS.Domain.ValueTypes;
using APSS.Web.Dtos;
using APSS.Web.Mvc.Auth;
using AutoMapper;

namespace APSS.Web.Mvc.Areas.Lands.Controllers
{
    [Area(Areas.Lands)]
    public class LandsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILandService _landSvc;
        private List<LandDto> _landList;

        public LandsController(ILandService landService, IMapper mapper)
        {
            _mapper = mapper;
            _landSvc = landService;
            _landList = new List<LandDto>();
        }

        public async Task<IActionResult> Index()
        {
            var result = await (await _landSvc.GetLandsAsync(User.GetAccountId(), User.GetUserId()))
                .AsAsyncEnumerable()
                .ToListAsync();

            return View(result.Select(_mapper.Map<LandDto>));
        }

        // GET: LandController/Add a new land
        [ApssAuthorized(AccessLevel.Farmer, PermissionType.Create)]
        public ActionResult Add()
        {
            return View(new LandDto());
        }

        // POST: LandController/Add a new land
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ApssAuthorized(AccessLevel.Farmer, PermissionType.Create)]
        public ActionResult Add(LandDto newLand)
        {
            if (!ModelState.IsValid || newLand == null)
            {
                return View(newLand);
            }
            Coordinates coordinates = new(newLand.Latitude, newLand.Longitude);
            _landSvc.AddLandAsync(User.GetAccountId(),
                newLand.Area,
                coordinates,
                newLand.Address,
                newLand.Name,
                newLand.IsUsable,
                newLand.IsUsed);

            return RedirectToAction(nameof(Index));
        }

        [ApssAuthorized(AccessLevel.Root | AccessLevel.Presedint | AccessLevel.Directorate | AccessLevel.District
                        | AccessLevel.Village | AccessLevel.Governorate | AccessLevel.Group | AccessLevel.Farmer, PermissionType.Read)]
        public async Task<ActionResult> Details(long Id)
        {
            if (Id <= 0)
            {
            }

            return View(_mapper.Map<LandDto>(
                await _landSvc.GetLandAsync(User.GetAccountId(), Id)));
        }

        // GET: LandController/Update land
        [ApssAuthorized(AccessLevel.Farmer, PermissionType.Update)]
        public async Task<ActionResult> Update(long Id)
        {
            if (Id <= 0)
            {
            }

            return View(_mapper.Map<LandDto>(
                await _landSvc.GetLandAsync(User.GetAccountId(), Id)));
        }

        // POST: LandController/Update land
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ApssAuthorized(AccessLevel.Farmer, PermissionType.Update)]
        public async Task<ActionResult> Update(LandDto landDto)
        {
            if (!ModelState.IsValid || landDto == null)
            {
            }
            await _landSvc.UpdateLandAsync(
                User.GetAccountId(),
                landDto!.Id,
                l =>
                {
                    l.Name = landDto.Name;
                    l.Longitude = landDto.Longitude;
                    l.IsUsed = landDto.IsUsed;
                    l.Latitude = landDto.Latitude;
                    l.Area = landDto.Area;
                    l.Address = landDto.Address;
                    l.IsUsable = landDto.IsUsable;
                });

            return RedirectToAction(nameof(Index));
        }

        // GET: LandController/Delete land
        [ApssAuthorized(AccessLevel.Farmer, PermissionType.Delete)]
        public async Task<ActionResult> Delete(long Id)
        {
            if (Id <= 0)
            {
            }

            return View(_mapper.Map<LandDto>(
                await _landSvc.GetLandAsync(User.GetAccountId(), Id)));
        }

        // POST: LandController/Delete land
        [HttpPost]
        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeletePost(long Id)
        {
            if (Id <= 0)
            {
            }
            await _landSvc.RemoveLandAsync(User.GetAccountId(), Id);

            return RedirectToAction(nameof(Index));
        }

        // GET: LandController/Get land
        [ApssAuthorized(AccessLevel.Farmer, PermissionType.Read)]
        public async Task<ActionResult> GetLand(long Id)
        {
            return View(_mapper.Map<LandDto>(
                await _landSvc.GetLandAsync(User.GetAccountId(), Id)));
        }

        // GET: LandController/Get lands
        public async Task<ActionResult> GetLands(long Id)
        {
            var landList = await _landSvc.GetLandsAsync(User.GetAccountId(), Id).ToAsyncEnumerable().ToListAsync();
            foreach (var land in landList)
            {
                var item = _mapper.Map<LandDto>(land);
                _landList.Add(item);
            }
            return View(_landList);
        }
    }
}