using Microsoft.AspNetCore.Mvc;
using APSS.Domain.Entities;
using APSS.Domain.Services;
using APSS.Domain.ValueTypes;
using APSS.Web.Dtos;
using APSS.Web.Dtos.Forms;
using APSS.Web.Mvc.Auth;
using APSS.Web.Mvc.Util.Navigation.Routes;
using AutoMapper;

namespace APSS.Web.Mvc.Areas.Lands.Controllers
{
    [Area(Areas.Lands)]
    public class LandsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILandService _landSvc;

        public LandsController(ILandService landService, IMapper mapper)
        {
            _mapper = mapper;
            _landSvc = landService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var result = await (await _landSvc.GetLandsAsync(User.GetAccountId(), User.GetUserId()))
                    .AsAsyncEnumerable()
                    .ToListAsync();

                return View(result.Select(_mapper.Map<LandDto>));
            }
            catch (Exception ex)
            {
                TempData["success"] = ex.ToString();
                return View();
            }
        }

        // GET: LandController/Add a new land
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        // POST: LandController/Add a new land
        [HttpPost]
        [ValidateAntiForgeryToken]
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

            return LocalRedirect(Routes.Dashboard.Lands.FullPath);
        }

        [HttpGet]
        public async Task<IActionResult> Details(long Id)
        {
            return View(_mapper.Map<LandDto>(
                await (await _landSvc.GetLandAsync(User.GetAccountId(), Id)).FirstAsync()));
        }

        // GET: LandController/Update land
        [HttpGet]
        public async Task<IActionResult> Update(long Id)
        {
            return View(_mapper.Map<LandDto>(
                await (await _landSvc.GetLandAsync(User.GetAccountId(), Id)).FirstAsync()));
        }

        // POST: LandController/Update land
        //[HttpPost("[action]/{landId}")]  [FromRoute] long landId,
        [HttpPost]
        [ValidateAntiForgeryToken]
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

            return LocalRedirect(Routes.Dashboard.Lands.FullPath);
        }

        // GET: LandController/Delete land
        //[ApssAuthorized(AccessLevel.Farmer, PermissionType.Delete)]
        public async Task<IActionResult> Delete(long Id)
        {
            return View(_mapper.Map<LandDto>(await (
                await _landSvc.GetLandAsync(User.GetAccountId(), Id)).FirstAsync()));
        }

        // POST: LandController/Delete land
        //[ApssAuthorized(AccessLevel.Farmer, PermissionType.Delete)]
        public async Task<IActionResult> DeletePost(long Id)
        {
            await _landSvc.RemoveLandAsync(User.GetAccountId(), Id);

            return LocalRedirect(Routes.Dashboard.Lands.FullPath);
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
            try
            {
                var landList = await (
                await _landSvc.GetLandsAsync(User.GetAccountId(), Id))
                .FirstAsync().
                ToAsyncEnumerable().ToListAsync();

                return View(landList.Select(_mapper.Map<LandDto>));
            }
            catch (Exception ex)
            {
                TempData["success"] = ex.ToString();
                return View(Id);
            }
        }
    }
}