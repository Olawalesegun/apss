﻿using Microsoft.AspNetCore.Mvc;
using APSS.Domain.Entities;
using APSS.Domain.Services;
using APSS.Web.Mvc.Auth;
using AutoMapper;
using APSS.Web.Dtos;
using APSS.Web.Mvc.Util.Navigation.Routes;

namespace APSS.Web.Mvc.Areas.Lands.Controllers
{
    [Area(Areas.Lands)]
    public class SeasonsController : Controller
    {
        private readonly ILandService _landSvc;
        private readonly IMapper _mapper;

        public SeasonsController(ILandService landService, IMapper mapper)
        {
            _landSvc = landService;
            _mapper = mapper;
        }

        //[ApssAuthorized(AccessLevel.Root, PermissionType.Read)]
        public async Task<IActionResult> Index()
        {
            var seasons = await _landSvc.GetSeasonsAsync()
                .AsAsyncEnumerable()
                .ToListAsync();

            return View(seasons.Select(_mapper.Map<SeasonDto>));
        }

        // GET: SeasonController/Add a new Season
        //[ApssAuthorized(AccessLevel.Root, PermissionType.Create)]
        public ActionResult Add()
        {
            return View();
        }

        // POST: SeasonController/Add a new Season
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[ApssAuthorized(AccessLevel.Root, PermissionType.Create)]
        public async Task<IActionResult> Add(SeasonDto season)
        {
            if (!ModelState.IsValid)
            { }
            await _landSvc.AddSeasonAsync(
                User.GetAccountId(),
                season!.Name,
                season.StartsAt,
                season.EndsAt);

            return LocalRedirect(Routes.Dashboard.Lands.Seasons.FullPath);
        }

        // GET: SeasonController/Update Season
        //[ApssAuthorized(AccessLevel.Root, PermissionType.Update)]
        public async Task<IActionResult> Update(long Id)
        {
            return View(_mapper.Map<SeasonDto>(
                await (await _landSvc.GetSeasonAsync(User.GetAccountId(), Id)).FirstAsync()));
        }

        // POST: SeasonController/Update Season
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[ApssAuthorized(AccessLevel.Root, PermissionType.Update)]
        public async Task<IActionResult> Update(SeasonDto season)
        {
            if (!ModelState.IsValid)
            { }
            await _landSvc.UpdateSeasonAsync(User.GetAccountId(),
                season!.Id,
                f =>
                {
                    f.Name = season.Name;
                    f.StartsAt = season.StartsAt;
                    f.EndsAt = season.EndsAt;
                });

            return LocalRedirect(Routes.Dashboard.Lands.Seasons.FullPath);
        }

        // GET: SeasonController/Delete Season
        //[ApssAuthorized(AccessLevel.Root, PermissionType.Delete)]
        public async Task<IActionResult> Delete(long Id)
        {
            return View(_mapper.Map<SeasonDto>(
                await (await _landSvc.GetSeasonAsync(User.GetAccountId(), Id)).FirstAsync()));
        }

        // POST: SeasonController/Delete Season
        //[ApssAuthorized(AccessLevel.Root, PermissionType.Delete)]
        public async Task<IActionResult> DeletePost(long Id)
        {
            await _landSvc.RemoveSeasonAsync(User.GetAccountId(), Id);

            return LocalRedirect(Routes.Dashboard.Lands.Seasons.FullPath);
        }

        // GET: SeasonController/Get Season
        //[ApssAuthorized(AccessLevel.Root, PermissionType.Read)]
        public async Task<IActionResult> GetSeason(long Id)
        {
            return View(_mapper.Map<SeasonDto>(
                await (await _landSvc.GetSeasonAsync(User.GetAccountId(), Id)).FirstAsync()));
        }
    }
}