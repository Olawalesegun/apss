using Microsoft.AspNetCore.Mvc;
using APSS.Domain.Entities;
using APSS.Domain.Services;
using APSS.Web.Mvc.Auth;
using AutoMapper;
using APSS.Web.Dtos;

namespace APSS.Web.Mvc.Areas.Lands.Controllers
{
    [Area(Areas.Lands)]
    public class SeasonsController : Controller
    {
        private readonly ILandService _landSvc;
        private readonly IMapper _mapper;
        private readonly List<SeasonDto> _seasonsList;

        public SeasonsController(ILandService landService, IMapper mapper)
        {
            _landSvc = landService;
            _mapper = mapper;
            _seasonsList = new List<SeasonDto>();
        }

        [ApssAuthorized(AccessLevel.Root, PermissionType.Read)]
        public async Task<IActionResult> Index()
        {
            var seasons = await _landSvc.GetSeasonsAsync()
                .AsAsyncEnumerable()
                .ToListAsync();
            foreach (var season in seasons)
            {
                var item = _mapper.Map<SeasonDto>(season);
                _seasonsList.Add(item);
            }

            return View(_seasonsList);
        }

        // GET: SeasonController/Add a new Season
        [ApssAuthorized(AccessLevel.Root, PermissionType.Create)]
        public ActionResult Add()
        {
            return View();
        }

        // POST: SeasonController/Add a new Season
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ApssAuthorized(AccessLevel.Root, PermissionType.Create)]
        public async Task<ActionResult> Add(SeasonDto season)
        {
            if (!ModelState.IsValid || season == null)
            { }
            await _landSvc.AddSeasonAsync(
                User.GetAccountId(),
                season!.Name,
                season.StartsAt,
                season.EndsAt);

            return RedirectToAction("Index");
        }

        // GET: SeasonController/Update Season
        [ApssAuthorized(AccessLevel.Root, PermissionType.Update)]
        public async Task<ActionResult> Update(long Id)
        {
            if (Id <= 0)
            { }

            return View(_mapper.Map<SeasonDto>(
                await _landSvc.GetSeasonAsync(User.GetAccountId(), Id)));
        }

        // POST: SeasonController/Update Season
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ApssAuthorized(AccessLevel.Root, PermissionType.Update)]
        public async Task<ActionResult> Update(SeasonDto season)
        {
            if (!ModelState.IsValid || season == null)
            { }
            await _landSvc.UpdateSeasonAsync(User.GetAccountId(),
                season!.Id,
                f =>
                {
                    f.Name = season.Name;
                    f.StartsAt = season.StartsAt;
                    f.EndsAt = season.EndsAt;
                });

            return RedirectToAction("Index");
        }

        // GET: SeasonController/Delete Season
        [ApssAuthorized(AccessLevel.Root, PermissionType.Delete)]
        public async Task<ActionResult> Delete(long Id)
        {
            return View(_mapper.Map<SeasonDto>(
                await _landSvc.GetSeasonAsync(User.GetAccountId(), Id)));
        }

        // POST: SeasonController/Delete Season
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [ApssAuthorized(AccessLevel.Root, PermissionType.Delete)]
        public async Task<ActionResult> DeletePost(long Id)
        {
            if (Id <= 0)
            { }
            await _landSvc.RemoveSeasonAsync(User.GetAccountId(), Id);

            return RedirectToAction("Index");
        }

        // GET: SeasonController/Get Season
        [ApssAuthorized(AccessLevel.Root, PermissionType.Read)]
        public async Task<ActionResult> GetSeason(long Id)
        {
            return View(_mapper.Map<SeasonDto>(
                await _landSvc.GetSeasonAsync(User.GetAccountId(), Id)));
        }
    }
}