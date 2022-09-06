using Microsoft.AspNetCore.Mvc;
using APSS.Domain.Entities;
using APSS.Domain.Services;
using APSS.Web.Mvc.Auth;
using AutoMapper;
using APSS.Web.Dtos;

namespace APSS.Web.Mvc.Areas.Lands.Controllers
{
    [Area(Areas.Lands)]
    public class ProductsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILandService _landSvc;
        private List<LandProduct> productsList;

        public ProductsController(ILandService landService, IMapper mapper)
        {
            _mapper = mapper;
            _landSvc = landService;
            productsList = new List<LandProduct>();
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
            var result = await (await _landSvc.GetLandProductsAsync(
                User.GetAccountId(), User.GetAccountId()))
                .AsAsyncEnumerable()
                .ToListAsync();

            return View(result.Select(_mapper.Map<LandProductDto>));
        }

        // GET: LandProduc tController/Add a new landProduct
        //[ApssAuthorized(AccessLevel.Farmer, PermissionType.Create)]
        public async Task<ActionResult> Add(long Id)
        {
            //var land = await _landSvc.GetLandAsync(User.GetId(), Id);
            var seasonsList = _landSvc.GetSeasonsAsync().AsAsyncEnumerable().ToListAsync();
            var unitsList = _landSvc.GetLandProductUnitsAsync().AsAsyncEnumerable().ToListAsync();

            var seasons = new List<SeasonDto>();
            var units = new List<LandProductUnitDto>();

            foreach (var season in await seasonsList)
            {
                var item = _mapper.Map<SeasonDto>(season);
                seasons.Add(item);
            }

            foreach (var unit in await unitsList)
            {
                var item = _mapper.Map<LandProductUnitDto>(unit);
                units.Add(item);
            }

            var product = new LandProductDto
            {
                //Producer = _mapper.Map<LandDto>(land),
                Units = units,
                Seasons = seasons,
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

            return RedirectToAction("Index", landProduct.Id);
        }

        [ApssAuthorized(
            AccessLevel.Presedint |
            AccessLevel.Directorate |
            AccessLevel.District |
            AccessLevel.Village |
            AccessLevel.Farmer |
            AccessLevel.Governorate |
            AccessLevel.Group |
            AccessLevel.Root,
            PermissionType.Read)]
        public async Task<ActionResult> Details(long Id)
        {
            if (Id <= 0)
            {
            }
            return View(_mapper.Map<LandProductDto>(
                await _landSvc.GetLandProductAsync(User.GetAccountId(), Id)));
        }

        // GET: LandController/Update landProduct
        [ApssAuthorized(AccessLevel.Farmer, PermissionType.Update)]
        public async Task<ActionResult> Update(long Id)
        {
            if (Id <= 0)
            {
            }
            return View(_mapper.Map<LandProductDto>(
                await _landSvc.GetLandProductAsync(User.GetAccountId(), Id)));
        }

        // POST: LandController/Update landProduct
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ApssAuthorized(AccessLevel.Farmer, PermissionType.Update)]
        public async Task<ActionResult> Update(LandProductDto landProduct)
        {
            if (!ModelState.IsValid || landProduct == null)
            {
            }
            await _landSvc.UpdateLandProductAsync(User.GetAccountId(), landProduct!.Id,
                f =>
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
                    //f.ProducedIn = _mapper.Map<Season>(landProduct.ProducedIn);
                    //f.Unit = _mapper.Map<LandProductUnit>(landProduct.Unit);
                });

            return RedirectToAction("Index");
        }

        // GET: LandController/Delete landProduct
        [ApssAuthorized(AccessLevel.Farmer, PermissionType.Delete)]
        public async Task<ActionResult> Delete(long Id)
        {
            return View(_mapper.Map<LandProductDto>(
                await _landSvc.GetLandProductAsync(User.GetAccountId(), Id)));
        }

        // POST: LandController/Delete landProduct
        [HttpPost]
        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Delete")]
        [ApssAuthorized(AccessLevel.Farmer, PermissionType.Delete)]
        public async Task<ActionResult> DeletePost(long Id)
        {
            await _landSvc.RemoveLandProductAsync(User.GetAccountId(), Id);
            return RedirectToAction(nameof(Index));
        }

        // GET: LandController/Get landProduct
        [ApssAuthorized(
            AccessLevel.Presedint |
            AccessLevel.Directorate |
            AccessLevel.District |
            AccessLevel.Village |
            AccessLevel.Farmer |
            AccessLevel.Governorate |
            AccessLevel.Group |
            AccessLevel.Root,
            PermissionType.Read)]
        public async Task<ActionResult> GetLandProduct(long Id)
        {
            return View(_mapper.Map<LandProductDto>(
                await _landSvc.GetLandProductAsync(User.GetAccountId(), Id)));
        }

        // GET: LandController/Get landProducts
        [ApssAuthorized(
            AccessLevel.Presedint |
            AccessLevel.Directorate |
            AccessLevel.District |
            AccessLevel.Village |
            AccessLevel.Farmer |
            AccessLevel.Governorate |
            AccessLevel.Group |
            AccessLevel.Root,
            PermissionType.Read)]
        public async Task<ActionResult> GetLandProducts(long Id)
        {
            var productsList = await _landSvc.GetLandProductsAsync(
                User.GetAccountId(), Id)
                .ToAsyncEnumerable()
                .ToListAsync();
            var products = new List<LandProductDto>();

            foreach (var product in productsList)
            {
                var item = _mapper.Map<LandProductDto>(product);
                products.Add(item);
            }

            return View(products);
        }
    }
}