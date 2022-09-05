using Microsoft.AspNetCore.Mvc;
using APSS.Domain.Entities;
using APSS.Domain.Services;
using APSS.Domain.ValueTypes;
using APSS.Web.Dtos;
using APSS.Web.Mvc.Auth;
using AutoMapper;
using APSS.Web.Mvc.Util.Navigation.Routes;
using APSS.Web.Dtos.Forms;

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
        [HttpGet]
        //[ApssAuthorized(AccessLevel.Farmer | AccessLevel.Root, PermissionType.Create)]
        public ActionResult Add()
        {
            return View();
        }

        // POST: LandController/Add a new land
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[ApssAuthorized(AccessLevel.Farmer | AccessLevel.Root, PermissionType.Create)]
        public async Task<IActionResult> Add([FromForm] AddLandForm newLand)
        {
            if (!ModelState.IsValid)
                return View(newLand);

            Coordinates coordinates = new(newLand.Latitude, newLand.Longitude);

            await _landSvc.AddLandAsync(
                User.GetAccountId(),
                newLand.Area,
                coordinates,
                newLand.Address,
                newLand.Name,
                newLand.IsUsable,
                newLand.IsUsed);

            return LocalRedirect(Routes.Dashboard.Lands.Lands.FullPath);
        }

        [HttpGet]
        //[ApssAuthorized(PermissionType.Read)]
        public async Task<IActionResult> Details(long Id)
        {
            return View(_mapper.Map<LandDto>(
                await (await _landSvc.GetLandAsync(User.GetAccountId(), Id)).FirstAsync()));
        }

        // GET: LandController/Update land
        [HttpGet]
        //[ApssAuthorized(AccessLevel.Farmer, PermissionType.Update)]
        public async Task<IActionResult> Update(long Id)
        {
            return View(_mapper.Map<LandDto>(
                await (await _landSvc.GetLandAsync(User.GetAccountId(), Id)).FirstAsync()));
        }

        // POST: LandController/Update land
        //[HttpPost("[action]/{landId}")]  [FromRoute] long landId,
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[ApssAuthorized(AccessLevel.Farmer, PermissionType.Update)]
        public async Task<IActionResult> Update([FromForm] UpdateLandForm form)
        {
            if (!ModelState.IsValid)
            {
            }
            await _landSvc.UpdateLandAsync(
                User.GetAccountId(),
                form.Id,
                l =>
                {
                    l.Name = form.Name;
                    l.Longitude = form.Longitude;
                    l.IsUsed = form.IsUsed;
                    l.Latitude = form.Latitude;
                    l.Area = form.Area;
                    l.Address = form.Address;
                    l.IsUsable = form.IsUsable;
                });

            return LocalRedirect(Routes.Dashboard.Lands.Lands.FullPath);
        }

        // GET: LandController/Delete land
        //[ApssAuthorized(AccessLevel.Farmer, PermissionType.Delete)]
        public async Task<IActionResult> Delete(long Id)
        {
            if (Id <= 0)
            {
            }

            return View(_mapper.Map<LandDto>(await (
                await _landSvc.GetLandAsync(User.GetAccountId(), Id)).FirstAsync()));
        }

        // POST: LandController/Delete land
        //[ApssAuthorized(AccessLevel.Farmer, PermissionType.Delete)]
        public async Task<IActionResult> DeletePost(long Id)
        {
            await _landSvc.RemoveLandAsync(User.GetAccountId(), Id);

            return LocalRedirect(Routes.Dashboard.Lands.Lands.FullPath);
        }

        // GET: LandController/Get land
        [ApssAuthorized(AccessLevel.Farmer, PermissionType.Read)]
        public async Task<ActionResult> GetLand(long Id)
        {
            return View(_mapper.Map<LandDto>(
                await (await _landSvc.GetLandAsync(User.GetAccountId(), Id)).FirstAsync()));
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