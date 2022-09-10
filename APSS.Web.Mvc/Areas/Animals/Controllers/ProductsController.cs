using Microsoft.AspNetCore.Mvc;
using APSS.Web.Dtos;
using APSS.Domain.Services;
using APSS.Web.Mvc.Auth;
using APSS.Web.Mvc.Util.Navigation.Routes;
using AutoMapper;

namespace APSS.Web.Mvc.Areas.Controllers
{
    [Area(Areas.Animals)]
    public class ProductsController : Controller
    {
        private IEnumerable<AnimalProductDto> product;
        private readonly IAnimalService _aps;
        private readonly IMapper _mapper;

        public ProductsController(IAnimalService aps, IMapper mapper)
        {
            _aps = aps;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var products = await (await _aps.GetAllAnimalProductsAsync(User.GetAccountId(), User.GetUserId()))
                .Include(u => u.Unit)
                .AsAsyncEnumerable()
                .ToListAsync();
            var productDto = new List<ProductListDto>();
            foreach (var product in products)
            {
                productDto.Add(new ProductListDto
                {
                    Name = product.Name,
                    Quantity = product.Quantity,
                    Id = product.Id,
                    PeriodTaken = product.PeriodTaken,
                    CreatedAt = product.CreatedAt,
                    ModifiedAt = product.ModifiedAt,
                    Unit = product.Unit,
                    IsConfirmed = product.IsConfirmed
                });
            }

            return View(productDto);
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
            product.ProducerId = id;
            product.Unit = unitList;

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

        /*public async Task<IActionResult> Index(string searchString, string searchBy)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.Trim();
                if (searchBy == "2")
                {
                    List<AnimalProductDto> response = new List<AnimalProductDto>();
                    response = product.Where(p => p.Id == Convert.ToInt32(searchString)).ToList();
                    return View(response);
                }
                else if (searchBy == "2")
                {
                    List<AnimalProductDto> response = new List<AnimalProductDto>();
                    response = product.Where(p => p.Quantity == Convert.ToInt32(searchString)).ToList();
                    return View(response);
                }
                else
                {
                    List<AnimalProductDto> response = new List<AnimalProductDto>();
                    response = product.Where(p => p.Name.Contains(searchString)).ToList();
                    return View(response);
                }
            }

            ViewBag.SearchResult = "ليس هناك نتائج عن " + searchString;

            var result = new List<AnimalProductDto>();
            return View(result);
        }
*/

        public async Task<IActionResult> Details(int id)
        {
            var product = await (await _aps.GetAnimalProductAsync(User.GetAccountId(), id))
                .Include(u => u.Unit)
                .Include(A => A.Producer)
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
            };
            return View(productDto);
        }

        public async Task<IActionResult> Edit(int id)
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
        public async Task<IActionResult> Edit(AnimalProductDto productObj)
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
            var delete = await (await _aps.GetAnimalProductAsync(3, id)).Include(u => u.Unit).AsAsyncEnumerable().ToListAsync();
            var single = delete.FirstOrDefault();
            var productDto = new AnimalProductDetailsDto
            {
                Id = single!.Id,
                Name = single.Name,
                Quantity = single.Quantity,
                PeriodTaken = single.PeriodTaken,
                UnitName = single.Unit.Name,
                CreatedAt = single.CreatedAt,
                ModifiedAt = single.ModifiedAt,
            };
            return View(productDto);
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

        public async Task<IActionResult> AllProducts()
        {
            AnimalGroupAndProductDto animalGroupProductDto = new AnimalGroupAndProductDto();
            AnimalProductDto animalproducts = new AnimalProductDto();
            animalGroupProductDto.AnimalProducts = animalproducts;

            return View(animalGroupProductDto);
        }

        public async Task<IActionResult> ListProductExpense(long id)
        {
            var expense = new List<ProductExpenseDto>();
            expense.Add(new ProductExpenseDto { Id = 1, Type = "شراء", Price = 10000 });
            return View(expense);
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