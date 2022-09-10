using Microsoft.AspNetCore.Mvc;
using APSS.Domain.Entities;
using APSS.Domain.Services;
using APSS.Domain.ValueTypes;
using APSS.Web.Dtos;
using APSS.Web.Dtos.Forms;
using APSS.Web.Mvc.Auth;
using APSS.Web.Mvc.Util.Navigation.Routes;
using AutoMapper;
using APSS.Web.Dtos.Parameters;
using APSS.Web.Mvc.Models;

namespace APSS.Web.Mvc.Areas.Confirmations.Controllers
{
    public class ConfirmationsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IConfirmSrevice _confirm;

        public ConfirmationsController(IConfirmSrevice confirm, IMapper mapper)
        {
            _mapper = mapper;
            _confirm = confirm;
        }

        public async Task<IActionResult> Index()
        {
            LandConfirmationDto landConfirmation = new LandConfirmationDto();

            landConfirmation.Products = await (
                await _confirm.UnConfirmedLandProductsAsync(User.GetAccountId()))
                .AsAsyncEnumerable()
                .Select(_mapper.Map<LandProductDto>)
                .ToListAsync();

            landConfirmation.Lands = await (
                await _confirm.UnConfirmedLandsAsync(User.GetAccountId()))
                .AsAsyncEnumerable()
                .Select(_mapper.Map<LandDto>)
                .ToListAsync();

            return View(landConfirmation);
        }

        [ApssAuthorized(AccessLevel.Group, PermissionType.Read)]
        public async Task<IActionResult> UnConfirmedLands()
        {
            var landList = await (
                await _confirm.UnConfirmedLandsAsync(User.GetAccountId()))
                .AsAsyncEnumerable()
                .ToListAsync();

            return View("UnConfirmedLands", landList.Select(_mapper.Map<LandDto>));
        }

        [HttpGet]
        [ApssAuthorized(AccessLevel.Group, PermissionType.Read)]
        public async Task<IActionResult> ConfirmedLands(long Id)
        {
            var landList = await (
                await _confirm.ConfirmedLandsAsync(User.GetAccountId()))
                .AsAsyncEnumerable()
                .ToListAsync();

            return View(landList.Select(_mapper.Map<LandDto>));
        }

        // GET: ProducsController/Get UnConfirmedLandProducts
        [HttpGet]
        //[ApssAuthorized(AccessLevel.Group, PermissionType.Read)]
        public async Task<IActionResult> UnConfirmedLandProducts()
        {
            var landProductList = await (
                await _confirm.UnConfirmedLandProductsAsync(User.GetAccountId()))
                .AsAsyncEnumerable()
                .ToListAsync();

            return View(landProductList.Select(_mapper.Map<LandProductDto>));
        }

        [HttpGet]
        //[ApssAuthorized(AccessLevel.Group, PermissionType.Read)]
        public async Task<IActionResult> ConfirmedLandProducts(long Id)
        {
            var landProductList = await (
                await _confirm.ConfirmedLandProductsAsync(User.GetAccountId()))
                .AsAsyncEnumerable()
                .ToListAsync();

            return View(landProductList.Select(_mapper.Map<LandProductDto>));
        }

        [HttpGet]
        //[ApssAuthorized(AccessLevel.Group, PermissionType.Update)]
        public async Task<IActionResult> ConfirmLand(long id, bool value)
        {
            await _confirm.ConfirmLandAsync(User.GetAccountId(), id, value);
            TempData["success"] = value ? "Land confirmed successfully" : "Land declined successfully";

            return View();
            /*return LocalRedirect(Routes.Dashboard.Confirmation.FullPath);*/
        }

        [HttpGet]
        //[ApssAuthorized(AccessLevel.Group, PermissionType.Update)]
        public async Task<IActionResult> ConfirmLandProduct(long id, bool value)
        {
            await _confirm.ConfirmLandProductAsync(User.GetAccountId(), id, value);

            //return LocalRedirect(Routes.Dashboard.Lands.FullPath);
            TempData["success"] = value ? "Product confirmed successfully" : "Product declined successfully";

            return View();
            /*return LocalRedirect(Routes.Dashboard.Confirmation.FullPath);*/
        }
    }
}