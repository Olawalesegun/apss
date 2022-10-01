using Microsoft.AspNetCore.Mvc;
using APSS.Web.Dtos;
using APSS.Domain.Services;
using APSS.Web.Mvc.Auth;
using APSS.Web.Mvc.Util.Navigation.Routes;
using APSS.Web.Dtos.Parameters;
using AutoMapper;
using APSS.Web.Mvc.Models;

namespace APSS.Web.Mvc.Areas.Controllers
{
    [Area(Areas.Animals)]
    public class ExpensesController : Controller
    {
        private readonly IAnimalService _pes;
        private readonly IMapper _mapper;

        public ExpensesController(IAnimalService pes, IMapper mapper)

        {
            _mapper = mapper;
            _pes = pes;
        }

        [HttpGet]
        public async Task<IActionResult> Index(long id, [FromQuery] FilteringParameters args)
        {
            var result = await (await _pes.GetProductExpenses(User.GetAccountId(), User.GetUserId(), id))
                  .Where(u => u.Type.Contains(args.Query ?? String.Empty))
                 .Page(args.Page, args.PageLength)
                  .AsAsyncEnumerable()
                  .Select(_mapper.Map<ProductExpenseDto>)
                  .ToListAsync();
            var expense = new List<ProductExpenseDto>();
            foreach (var item in result)
            {
                expense.Add(new ProductExpenseDto
                {
                    Price = item.Price,
                    Type = item.Type,
                    CreatedAt = item.CreatedAt,
                    ModifiedAt = item.ModifiedAt,
                    Id = item.Id,
                });
            }
            return View(new CrudViewModel<ProductExpenseDto>(result, args));
        }

        [HttpGet]
        public async Task<IActionResult> ListExpense(long userId, long productId)
        {
            var result = await (await _pes.GetProductExpenses(User.GetAccountId(), userId, productId)).AsAsyncEnumerable().ToListAsync();
            var expence = new List<ProductExpenseDto>();
            if (result.Any())
            {
                foreach (var item in result)
                {
                    expence.Add(new ProductExpenseDto
                    {
                        Price = item.Price,
                        Type = item.Type,
                        CreatedAt = item.CreatedAt,
                        ModifiedAt = item.ModifiedAt,
                        Id = item.Id
                    });
                }
            }
            return View(expence);
        }

        public IActionResult Add(long id)
        {
            var expense = new ProductExpenseDto();
            expense.ProductId = id;
            return View(expense);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(ProductExpenseDto productExpense)
        {
            if (!ModelState.IsValid)
            {
                var add = await _pes.CreateProductExpenseAsync(User.GetAccountId(),
                    productExpense.ProductId,
                    productExpense.Type,
                    productExpense.Price);
                return LocalRedirect(Routes.Dashboard.Animals.Expense.Details + $"?id={add.Id}");
            }
            else return View(productExpense);
        }

        [HttpGet]
        public async Task<ActionResult> Details(long id)
        {
            var getExpense = await (await _pes.GetExpense(User.GetAccountId(), id)).Where(i => i.Id == id).AsAsyncEnumerable().ToListAsync();
            var result = getExpense.FirstOrDefault();
            var expense = new ProductExpenseDto
            {
                Id = result!.Id,
                Type = result.Type,
                CreatedAt = result.CreatedAt,
                ModifiedAt = result.ModifiedAt,
            };

            return View(expense);
        }

        // GET: landProductExpense/Update landProductExpense
        public async Task<IActionResult> Update(long id)
        {
            var update = await (await _pes.GetExpense(User.GetAccountId(), id)).AsAsyncEnumerable().ToListAsync();
            var single = update.FirstOrDefault();
            var expense = new ProductExpenseDto
            {
                Price = single!.Price,
                Id = single.Id,
                Type = single.Type,
            };
            expense.ProductId = id;
            return View(expense);
        }

        // POST: landProductExpense/Update landProductExpense
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(ProductExpenseDto productExpense)
        {
            var update = await _pes.UpdateProductExpensesAsync(User.GetAccountId(), productExpense.ProductId, expense =>
              {
                  expense.Price = productExpense.Price;
                  expense.Type = productExpense.Type;
              });
            return LocalRedirect(Routes.Dashboard.Animals.Expense.Details + $"?id={update.Id}");
        }

        /*  public async Task<ActionResult> Delete(long id)
          {
              var delete = await (await _pes.GetExpense(User.GetAccountId(), id)).AsAsyncEnumerable().ToListAsync();
              var single = delete.FirstOrDefault();

              var expense = new ProductExpenseDto
              {
                  Price = single!.Price,
                  Id = single.Id,
                  Type = single.Type,
              };
              return View(expense);
          }*/

        public async Task<ActionResult> Delete(long id)
        {
            await _pes.RemoveProductExpenseAsync(User.GetAccountId(), id);
            return LocalRedirect(Routes.Dashboard.Animals.Products.FullPath);
        }
    }
}