using Microsoft.AspNetCore.Mvc;
using APSS.Domain.Entities;
using APSS.Domain.Services;
using APSS.Web.Mvc.Auth;
using AutoMapper;
using APSS.Web.Dtos;
using APSS.Web.Mvc.Util.Navigation.Routes;

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

        //[ApssAuthorized(
        //    AccessLevel.Presedint |
        //    AccessLevel.Directorate |
        //    AccessLevel.District |
        //    AccessLevel.Village |
        //    AccessLevel.Farmer |
        //    AccessLevel.Governorate |
        //    AccessLevel.Group |
        //    AccessLevel.Root,
        //    PermissionType.Read)]
        public async Task<IActionResult> Index()
        {
            try
            {
                var result = await (await _landSvc.GetAllLandProductsAsync(
                User.GetAccountId()))
                .AsAsyncEnumerable()
                .ToListAsync();

                return View(result.Select(_mapper.Map<LandProductDto>));
            }
            catch (Exception ex)
            {
                return View(new List<LandProductDto>());
            }
        }

        // GET: LandProduc tController/Add a new landProduct
        //[ApssAuthorized(AccessLevel.Farmer, PermissionType.Create)]
        public async Task<ActionResult> Add(long Id)
        {
            var seasonsList = new List<Season>();
            var unitsList = new List<LandProductUnit>();
            try
            {
                seasonsList = await _landSvc.GetSeasonsAsync().AsAsyncEnumerable().ToListAsync();
                unitsList = await _landSvc.GetLandProductUnitsAsync().AsAsyncEnumerable().ToListAsync();
            }
            catch (Exception ex)
            {
                return View("ErrorPage", ex.ToString());
            }
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
        //[ApssAuthorized(AccessLevel.Farmer, PermissionType.Create)]
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

            return LocalRedirect(Routes.Dashboard.Lands.Products.FullPath);
        }

        //[ApssAuthorized(
        //    AccessLevel.Presedint |
        //    AccessLevel.Directorate |
        //    AccessLevel.District |
        //    AccessLevel.Village |
        //    AccessLevel.Farmer |
        //    AccessLevel.Governorate |
        //    AccessLevel.Group |
        //    AccessLevel.Root,
        //    PermissionType.Read)]
        public async Task<ActionResult> Details(long Id)
        {
            try
            {
                return View(_mapper.Map<LandProductDto>(
                await (await _landSvc.GetLandProductAsync(User.GetAccountId(), Id)).FirstAsync()));
            }
            catch (Exception ex)
            {
                return View("ErrorPage", ex.ToString());
            }
        }

        // GET: LandController/Update landProduct
        //[ApssAuthorized(AccessLevel.Farmer, PermissionType.Update)]
        public async Task<ActionResult> Update(long Id)
        {
            var products = _mapper.Map<LandProductDto>(
                await (await _landSvc.GetLandProductAsync(User.GetAccountId(), Id)).FirstAsync());

            return View(products);
        }

        // POST: LandController/Update landProduct
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[ApssAuthorized(AccessLevel.Farmer, PermissionType.Update)]
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

            return LocalRedirect(Routes.Dashboard.Lands.Products.FullPath);
        }

        // GET: LandController/Delete landProduct
        //[ApssAuthorized(AccessLevel.Farmer, PermissionType.Delete)]
        public async Task<ActionResult> Delete(long Id)
        {
            try
            {
                return View(_mapper.Map<LandProductDto>(
                await (await _landSvc.GetLandProductAsync(User.GetAccountId(), Id)).FirstAsync()));
            }
            catch (Exception ex)
            {
                return View("ErrorPage", ex.ToString());
            }
        }

        // POST: LandController/Delete landProduct
        //[ApssAuthorized(AccessLevel.Farmer, PermissionType.Delete)]
        public async Task<ActionResult> DeletePost(long Id)
        {
            try
            {
                await _landSvc.RemoveLandProductAsync(User.GetAccountId(), Id);
                return LocalRedirect(Routes.Dashboard.Lands.Products.FullPath);
            }
            catch (Exception ex)
            {
                return View("ErrorPage", ex.ToString());
            }
        }

        // GET: LandController/Get landProduct
        //[ApssAuthorized(
        //    AccessLevel.Presedint |
        //    AccessLevel.Directorate |
        //    AccessLevel.District |
        //    AccessLevel.Village |
        //    AccessLevel.Farmer |
        //    AccessLevel.Governorate |
        //    AccessLevel.Group |
        //    AccessLevel.Root,
        //    PermissionType.Read)]
        public async Task<ActionResult> GetLandProduct(long Id)
        {
            try
            {
                return View(_mapper.Map<LandProductDto>(
                await (await _landSvc.GetLandProductAsync(User.GetAccountId(), Id)).FirstAsync()));
            }
            catch (Exception ex)
            {
                return View("ErrorPage", ex.ToString());
            }
        }

        // GET: LandController/Get landProducts
        //[ApssAuthorized(
        //    AccessLevel.Presedint |
        //    AccessLevel.Directorate |
        //    AccessLevel.District |
        //    AccessLevel.Village |
        //    AccessLevel.Farmer |
        //    AccessLevel.Governorate |
        //    AccessLevel.Group |
        //    AccessLevel.Root,
        //    PermissionType.Read)]
        public async Task<ActionResult> GetLandProducts(long Id)
        {
            try
            {
                var result = await (await _landSvc.GetLandProductsAsync(
                User.GetAccountId(), Id))
                .AsAsyncEnumerable()
                .ToListAsync();

                return View(result.Select(_mapper.Map<LandProductDto>));
            }
            catch (Exception ex)
            {
                return View("ErrorPage", ex.ToString());
            }
        }

        // GET: ProducsController/Get UnConfirmedLandProducts
        [HttpGet]
        //[ApssAuthorized(AccessLevel.Group, PermissionType.Read)]
        public async Task<IActionResult> UnConfirmedProducts()
        {
            var landProductList = await (
                await _landSvc.UnConfirmedProductsAsync(User.GetAccountId()))
                .AsAsyncEnumerable()
                .ToListAsync();

            return View(landProductList.Select(_mapper.Map<LandProductDto>));
        }

        [HttpGet]
        //[ApssAuthorized(AccessLevel.Group, PermissionType.Read)]
        public async Task<IActionResult> ConfirmedProducts(long Id)
        {
            var landProductList = await (
                await _landSvc.ConfirmedProductsAsync(User.GetAccountId()))
                .AsAsyncEnumerable()
                .ToListAsync();

            return View(landProductList.Select(_mapper.Map<LandProductDto>));
        }

        [HttpGet]
        //[ApssAuthorized(AccessLevel.Group, PermissionType.Update)]
        public async Task<IActionResult> ConfirmProduct(long id, bool value)
        {
            await _landSvc.ConfirmProductAsync(User.GetAccountId(), id, value);

            //return LocalRedirect(Routes.Dashboard.Lands.FullPath);
            TempData["success"] = value ? "Product confirmed successfully" : "Product declined successfully";

            if (value)
                return RedirectToAction("DeclinedProducts");
            else
                return RedirectToAction("ConfirmedProducts");
        }
    }
}