﻿using Microsoft.AspNetCore.Mvc;
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
            var result = await (await _landSvc.GetLandsAsync(User.GetAccountId(), User.GetUserId()))
                .AsAsyncEnumerable()
                .ToListAsync();

            return View(result.Select(_mapper.Map<LandDto>));
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

            TempData["success"] = "Land added successfully";
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

            TempData["success"] = "Land updateded successfully";
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
            TempData["success"] = "Land deleted successfully";

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
        public async Task<IActionResult> GetLands(long Id)
        {
            var landList = await (
            await _landSvc.GetLandsAsync(User.GetAccountId(), Id))
            .FirstAsync().
            ToAsyncEnumerable().ToListAsync();

            return View(landList.Select(_mapper.Map<LandDto>));
        }

        //[ApssAuthorized(AccessLevel.Group, PermissionType.Read)]
        public async Task<IActionResult> UnConfirmedLands()
        {
            var landList = await (
                await _landSvc.UnConfirmedLandsAsync(User.GetAccountId()))
                .AsAsyncEnumerable()
                .ToListAsync();

            return View("UnConfirmedLands", landList.Select(_mapper.Map<LandDto>));
        }

        [HttpGet]
        //[ApssAuthorized(AccessLevel.Group, PermissionType.Read)]
        public async Task<IActionResult> ConfirmedLands(long Id)
        {
            var landList = await (
                await _landSvc.ConfirmedLandsAsync(User.GetAccountId()))
                .AsAsyncEnumerable()
                .ToListAsync();

            return View(landList.Select(_mapper.Map<LandDto>));
        }

        [HttpGet]
        //[ApssAuthorized(AccessLevel.Group, PermissionType.Update)]
        public async Task<IActionResult> ConfirmLand(long id, bool value)
        {
            await _landSvc.ConfirmLandAsync(User.GetAccountId(), id, value);
            TempData["success"] = value ? "Land confirmed successfully" : "Land declined successfully";

            //return LocalRedirect(Routes.Dashboard.Users.FullPath);
            if (value)
                return RedirectToAction("DeclinedLands");
            else
                return RedirectToAction("ConfirmedLands");
        }
    }
}