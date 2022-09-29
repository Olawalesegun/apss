using Microsoft.AspNetCore.Mvc;
using APSS.Web.Dtos;
using APSS.Domain.Services;
using APSS.Web.Mvc.Auth;
using APSS.Web.Mvc.Util.Navigation.Routes;
using AutoMapper;
using APSS.Web.Dtos.Parameters;
using APSS.Web.Mvc.Models;

namespace APSS.Web.Mvc.Areas.Controllers
{
    [Area(Areas.Animals)]
    public class ProductsController : Controller
    {
        private readonly IAnimalService _aps;
        private readonly IMapper _mapper;

        public ProductsController(IAnimalService aps, IMapper mapper)
        {
            _aps = aps;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index([FromQuery] FilteringParameters args)
        {
            var products = await (await _aps.GetAllAnimalProductsAsync(User.GetAccountId(), User.GetUserId()))
                .Include(u => u.Unit)
                /*  .Where(u => u.Name.Contains(args.Query))
                  .Page(args.Page, args.PageLength)*/
                .AsAsyncEnumerable()
                .Select(_mapper.Map<AnimalProductDetailsDto>)
                .ToListAsync();

            return View(new CrudViewModel<AnimalProductDetailsDto>(products, args));
        }

        public async Task<IActionResult> Add(int id)
        {
            var product = new AnimalProductDto();
            var units = await (await _aps.GetAnimalProductUnitAsync(id)).AsAsyncEnumerable().ToListAsync();
            var unitList = new List<AnimalProductUnitDto>();
            foreach (var unit in units)
            {
                unitList.Add(new AnimalProductUnitDto
                {
                    Name = unit.Name,
                    Id = unit.Id,
                });
            }
            ViewBag.units = unitList;
            product.ProducerId = id;

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AnimalProductDto product)
        {
            var add = await _aps.AddAnimalProductAsync(User.GetAccountId(),
                product.ProducerId,
                product.UnitId,
                product.Name,
                product.Quantity,
                product.PeriodTaken);
            return LocalRedirect(Routes.Dashboard.Animals.Products.FullPath);
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await (await _aps.GetAnimalProductAsync(User.GetAccountId(), id))
                .Include(u => u.Unit)
                .Include(A => A.Producer)
                .Include(E => E.Expenses)
                .AsAsyncEnumerable().ToListAsync();
            var single = product.FirstOrDefault();
            var productDto = new AnimalProductDetailsDto
            {
                Id = single!.Id,
                Name = single.Name,
                Quantity = single.Quantity,
                PeriodTaken = single.PeriodTaken,
                CreatedAt = single.CreatedAt,
                ModifiedAt = single.ModifiedAt,
                Unit = single.Unit,
                Producer = single.Producer,
                expense = single.Expenses.ToList(),
            };
            return View(productDto);
        }

        public async Task<IActionResult> Update(int id)
        {
            var units = await (await _aps.GetAnimalProductUnitAsync(User.GetAccountId()))
                .AsAsyncEnumerable()
                .ToListAsync();
            var unitDto = new List<AnimalProductUnitDto>();
            foreach (var unit in units)
            {
                unitDto.Add(new AnimalProductUnitDto
                {
                    Name = unit.Name,
                    Id = id,
                });
            }
            var edit = await (await _aps.GetAnimalProductAsync(User.GetAccountId(), id))
                .AsAsyncEnumerable()
                .ToListAsync();
            var single = edit.FirstOrDefault();
            var productDto = new AnimalProductDto
            {
                Id = single!.Id,
                Name = single.Name,
                Quantity = single.Quantity,
                PeriodTaken = single.PeriodTaken,
            };
            ViewBag.Units = unitDto;
            return View(productDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(AnimalProductDto productObj)
        {
            var unit = await (await _aps.GetAnimalProductUnitAsync(User.GetAccountId()))
                .Where(i => i.Id == productObj.UnitId)
                .AsAsyncEnumerable()
                .ToListAsync();
            var singleUnit = unit.FirstOrDefault();
            var edit = await _aps.UpdateAnimalProductAsync(User.GetAccountId(), productObj.Id, p =>
              {
                  p.Name = productObj.Name;
                  p.Quantity = productObj.Quantity;
                  p.PeriodTaken = productObj.PeriodTaken;
                  p.Unit = singleUnit!;
                  p.IsConfirmed = null;
              });
            return LocalRedirect(Routes.Dashboard.Animals.Products.FullPath);
        }

        public async Task<IActionResult> Delete(int id)
        {
            /*var delete = await (await _aps.GetAnimalProductAsync(User.GetAccountId(), id)).Include(u => u.Unit).AsAsyncEnumerable().ToListAsync();
            var single = delete.FirstOrDefault();
            var productDto = new AnimalProductDetailsDto
            {
                Id = single!.Id,
                Name = single.Name,
                Quantity = single.Quantity,
                UnitName = single.Unit.Name,
                PeriodTaken = single.PeriodTaken,
                CreatedAt = single.CreatedAt,
                ModifiedAt = single.ModifiedAt,
            };
            return View(productDto);*/

            try
            {
                if (id > 0)
                {
                    await _aps.RemoveAnimalProductAsync(User.GetAccountId(), id);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception) { return BadRequest(); }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteConfirm(int id)
        {
            try
            {
                if (id > 0)
                {
                    await _aps.RemoveAnimalProductAsync(User.GetAccountId(), id);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception) { return BadRequest(); }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(long id, [FromQuery] FilteringParameters args)
        {
            var products = await (await _aps.GetSpecificAnimalProductsAsync(User.GetAccountId(), id))
                  .Include(u => u.Unit)
                  /* .Where(u => u.Name.Contains(args.Query))
                   .Page(args.Page, args.PageLength)*/
                  .AsAsyncEnumerable()
                  .Select(_mapper.Map<AnimalProductDetailsDto>)
                  .ToListAsync();

            return View(new CrudViewModel<AnimalProductDetailsDto>(products, args));
        }

        public async Task<IActionResult> ProductList(long id)
        {
            var product = await (await _aps.GetAllAnimalProductsAsync(User.GetAccountId(), id)).Include(o => o.Producer.OwnedBy).Include(u => u.Unit).AsAsyncEnumerable().ToListAsync();
            var productDto = new List<AnimalProductDetailsDto>();
            foreach (var item in product)
            {
                productDto.Add(new AnimalProductDetailsDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    CreatedAt = item.CreatedAt,
                    ModifiedAt = item.ModifiedAt,
                    Quantity = item.Quantity,
                    Producer = item.Producer,
                    Unit = item.Unit,
                    UnitName = item.Unit.Name,
                    PeriodTaken = item.PeriodTaken,
                });
            }
            return View(productDto);
        }
    }
}