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
    public class ProductsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILandService _landSvc;

        public ProductsController(ILandService landService, IMapper mapper)
        {
            _mapper = mapper;
            _landSvc = landService;
        }

        [ApssAuthorized(AccessLevel.Farmer, PermissionType.Read)]
        public async Task<IActionResult> Index([FromQuery] FilteringParameters args)
        {
            var result = await (await _landSvc.GetProductsByUserIdAsync(
            User.GetAccountId(), User.GetUserId()))
            .Where(u => u.CropName.Contains(args.Query))
            .Page(args.Page, args.PageLength)
            .AsAsyncEnumerable()
            .Select(_mapper.Map<LandProductDto>)
            .ToListAsync();

            return View(new CrudViewModel<LandProductDto>(result, args));
        }

        // GET: LandProduc tController/Add a new landProduct
        [ApssAuthorized(AccessLevel.Farmer, PermissionType.Create)]
        public async Task<ActionResult> Add(long Id)
        {
            var seasonsList = await _landSvc.GetSeasonsAsync().AsAsyncEnumerable().ToListAsync();
            var unitsList = await _landSvc.GetLandProductUnitsAsync().AsAsyncEnumerable().ToListAsync();

            var product = new LandProductDto
            {
                Units = unitsList.Select(_mapper.Map<LandProductUnitDto>),
                Seasons = seasonsList.Select(_mapper.Map<SeasonDto>),
                landId = Id,
            };

            return View(product);
        }

        // POST: LandController/Add a new landProduct
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ApssAuthorized(AccessLevel.Farmer, PermissionType.Create)]
        public async Task<ActionResult> Add(LandProductDto landProduct)
        {
            if (!ModelState.IsValid)
            {
            }
            await _landSvc.AddLandProductAsync(
                User.GetAccountId(),
                landProduct!.landId,
                landProduct.SeasonId,
                landProduct.UnitId,
                landProduct.CropName,
                landProduct.HarvestStart,
                landProduct.HarvestEnd,
                landProduct.Quantity,
                landProduct.IrrigationCount,
                landProduct.IrrigationWaterSource,
                landProduct.IrrigationPowerSource,
                landProduct.IsGovernmentFunded,
                landProduct.StoringMethod,
                landProduct.Category,
                landProduct.HasGreenhouse,
                landProduct.Fertilizer,
                landProduct.Insecticide,
                landProduct.IrrigationMethod);

            TempData["success"] = "Product added";
            return LocalRedirect(Routes.Dashboard.Lands.Products.FullPath);
        }

        [ApssAuthorized(AccessLevel.All, PermissionType.Read)]
        public async Task<ActionResult> Details(long Id)
        {
            return View(_mapper.Map<LandProductDto>(
            await (await _landSvc.GetLandProductAsync(User.GetAccountId(), Id)).FirstAsync()));
        }

        // GET: LandController/Update landProduct
        [ApssAuthorized(AccessLevel.Farmer, PermissionType.Update)]
        public async Task<ActionResult> Update(long Id)
        {
            var products = _mapper.Map<LandProductDto>(
                await (await _landSvc.GetLandProductAsync(User.GetAccountId(), Id)).FirstAsync());

            return View(products);
        }

        // POST: LandController/Update landProduct
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ApssAuthorized(AccessLevel.Farmer, PermissionType.Update)]
        public async Task<ActionResult> Update(LandProductDto landProduct)
        {
            if (!ModelState.IsValid)
            {
            }
            await _landSvc.UpdateLandProductAsync(
                User.GetAccountId(),
                landProduct.Id,
            async f =>
            {
                f.StoringMethod = landProduct.StoringMethod;
                f.Category = landProduct.Category;
                f.CropName = landProduct.CropName;
                f.Fertilizer = landProduct.Fertilizer;
                f.HarvestEnd = landProduct.HarvestEnd;
                f.HarvestStart = landProduct.HarvestStart;
                f.HasGreenhouse = landProduct.HasGreenhouse;
                f.Insecticide = landProduct.Insecticide;
                f.IrrigationCount = landProduct.IrrigationCount;
                f.IrrigationMethod = landProduct.IrrigationMethod;
                f.IrrigationPowerSource = _mapper.Map<IrrigationPowerSource>(landProduct.IrrigationPowerSource);
                f.IrrigationWaterSource = _mapper.Map<IrrigationWaterSource>(landProduct.IrrigationWaterSource);
                f.IsGovernmentFunded = landProduct.IsGovernmentFunded;
                f.Quantity = landProduct.Quantity;
            });
            TempData["success"] = "Product updated";

            return LocalRedirect(Routes.Dashboard.Lands.Products.FullPath);
        }

        // GET: LandController/Delete landProduct
        [HttpPost]
        [ApssAuthorized(AccessLevel.Farmer, PermissionType.Delete)]
        public async Task<ActionResult> Delete(long Id)
        {
            await _landSvc.RemoveLandProductAsync(User.GetAccountId(), Id);

            TempData["success"] = "Product removed";
            return LocalRedirect(Routes.Dashboard.Lands.Products.FullPath);
        }

        // POST: LandController/Delete landProduct
        /*[ApssAuthorized(AccessLevel.Farmer, PermissionType.Delete)]
        public async Task<ActionResult> DeletePost(long Id)
        {
            await _landSvc.RemoveLandProductAsync(User.GetAccountId(), Id);

            TempData["success"] = "Product removed";
            return LocalRedirect(Routes.Dashboard.Lands.Products.FullPath);
        }*/

        [ApssAuthorized(AccessLevel.Farmer, PermissionType.Read)]
        public async Task<ActionResult> byLand([FromQuery] FilteringParameters args, long Id)
        {
            var result = await (await _landSvc.GetLandProductsAsync(
                User.GetAccountId(), Id))
                .Where(n => n.CropName.Contains(args.Query))
                .Page(args.Page, args.PageLength)
                .AsAsyncEnumerable()
                .Select(_mapper.Map<LandProductDto>)
                .ToListAsync();

            return View(new CrudViewModel<LandProductDto>(result, args));
        }

        [ApssAuthorized(AccessLevel.All, PermissionType.Read)]
        public async Task<ActionResult> byUser([FromQuery] FilteringParameters args, long Id)
        {
            var result = await (await _landSvc.GetProductsByUserIdAsync(
                User.GetAccountId(), Id))
                .Where(n => n.CropName.Contains(args.Query))
                .Page(args.Page, args.PageLength)
                .AsAsyncEnumerable()
                .Select(_mapper.Map<LandProductDto>)
                .ToListAsync();

            return View(new CrudViewModel<LandProductDto>(result, args));
        }
    }
}