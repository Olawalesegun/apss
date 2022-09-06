using Microsoft.AspNetCore.Mvc;
using APSS.Web.Dtos;
using APSS.Domain.Services;
using APSS.Web.Mvc.Auth;

namespace APSS.Web.Mvc.Areas.Controllers
{
    [Area(Areas.Animals)]
    public class ExpensesController : Controller
    {
        private readonly IAnimalService _pes;

        public ExpensesController(IAnimalService pes)
        {
            _pes = pes;
        }

        public async Task<IActionResult> Index()
        {
            var result = await (await _pes.GetProductExpenses(3, 3, 10)).AsAsyncEnumerable().ToListAsync();
            var expense = new List<ProductExpenseDto>();
            foreach (var item in result)
            {
                expense.Add(new ProductExpenseDto
                {
                    Price = item.Price,
                    Type = item.Type,
                    CreatedAt = item.CreatedAt,
                    ModifiedAt = item.ModifiedAt,
                    Id = item.Id
                });
            }
            return View(expense);
        }

        public async Task<IActionResult> ListExpense(long Id)
        {
            var result = await (await _pes.GetProductExpenses(3, 3, Id)).AsAsyncEnumerable().ToListAsync();
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
            else
            {
                return BadRequest();
            }
            //var expense = new List<ProductExpenseDto>();
            return View(expence);
        }

        public async Task<IActionResult> Add(long id)
        {
            var expense = new ProductExpenseDto();
            expense.ProductId = id;
            return View(expense);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(ProductExpenseDto productExpense)
        {
            try
            {
                var add = await _pes.CreateProductExpenseAsync(3,
                    productExpense.ProductId,
                    productExpense.Type,
                    productExpense.Price);
                if (add == null) return View("Error");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        public ActionResult Details(long Id)
        {
            var productExpenses = new List<ProductExpenseDto>();

            return View(productExpenses.Where(i => i.Id == Id).First());
        }

        // GET: landProductExpense/Update landProductExpense
        public async Task<IActionResult> Update(long id)
        {
            var update = await (await _pes.GetExpense(3, id)).AsAsyncEnumerable().ToListAsync();
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
            try
            {
                var update = await _pes.UpdateProductExpensesAsync(3, productExpense.ProductId, expense =>
                  {
                      expense.Price = productExpense.Price;
                      expense.Type = productExpense.Type;
                  });
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(productExpense);
            }
        }

        // GET: landProductExpense/Delete landProductExpense
        public async Task<ActionResult> Delete(long id)
        {
            try
            {
                var delete = await (await _pes.GetExpense(3, id)).AsAsyncEnumerable().ToListAsync();
                var single = delete.FirstOrDefault();
                if (single != null)
                {
                    var expense = new ProductExpenseDto
                    {
                        Price = single!.Price,
                        Id = single.Id,
                        Type = single.Type,
                    };
                    return View(expense);
                }
                return View();
            }
            catch (Exception)
            {
            }

            return View();
        }

        // POST: landProductExpense/Delete landProductExpense
        [HttpGet]
        public async Task<ActionResult> DeleteConfirm(long id)
        {
            try
            {
                await _pes.RemoveProductExpenseAsync(3, id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}