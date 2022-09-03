using Microsoft.AspNetCore.Mvc;
using APSS.Web.Dtos;

namespace APSS.Web.Mvc.Controllers
{
    public class ProductExpensesController : Controller
    {
        public async Task<IActionResult> Index(long id)
        {
            var expense = new List<ProductExpenseDto>();
            expense.Add(new ProductExpenseDto { Id = 1, Type = "type 1", Price = 10000, ProductId = id, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now });
            expense.Add(new ProductExpenseDto { Id = 2, Type = "type 2", Price = 10000, ProductId = id, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now });
            expense.Add(new ProductExpenseDto { Id = 3, Type = "type 3", Price = 10000, ProductId = id, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now });
            expense.Add(new ProductExpenseDto { Id = 4, Type = "type 4", Price = 10000, ProductId = id, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now });
            return View(expense);
        }

        public async Task<IActionResult> AddExpense(long id)
        {
            var expense = new ProductExpenseDto();
            return View(expense);
        }

        [HttpPost]
        public async Task<IActionResult> AddExpense(ProductExpenseDto expense)
        {
            return View(expense);
        }

        public async Task<IActionResult> EditExpense(long id)
        {
            var expense = new ProductExpenseDto();
            return View(expense);
        }

        [HttpPost]
        public async Task<IActionResult> EditExpense(ProductExpenseDto expense)
        {
            return View(expense);
        }

        public async Task<IActionResult> DeleteExpense(long id)
        {
            var expense = new ProductExpenseDto { Id = 1, Type = "type 1", Price = 10000, ProductId = id, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now };
            return View(expense);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteExpense(ProductExpensesController expense)
        {
            var expenses = new ProductExpensesController();

            return RedirectToAction("Index");
        }
    }
}