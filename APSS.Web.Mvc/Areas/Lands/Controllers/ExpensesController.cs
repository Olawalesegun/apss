using Microsoft.AspNetCore.Mvc;
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
    public class ExpensesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILandService _landSvc;

        public ExpensesController(ILandService landService, IMapper mapper)
        {
            _mapper = mapper;
            _landSvc = landService;
        }

        public async Task<IActionResult> Index(long Id)
        {
            try
            {
                var expenses = await (
                    await _landSvc.GetLandProductExpensesAsync(User.GetAccountId(), Id))
                    .FirstAsync()
                    .ToAsyncEnumerable()
                    .ToListAsync();

                return View(expenses.Select(_mapper.Map<ProductExpenseDto>));
            }
            catch (Exception ex)
            {
                TempData["success"] = ex.ToString();

                return NotFound();
            }
        }

        [HttpGet]
        //[ApssAuthorized(AccessLevel.Farmer, PermissionType.Create)]
        public IActionResult Add(long Id)
        {
            var expense = new ProductExpenseDto
            {
                ProductId = Id,
            };

            return View(expense);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[ApssAuthorized(AccessLevel.Farmer, PermissionType.Create)]
        public async Task<ActionResult> Add(ProductExpenseDto productExpense)
        {
            if (!ModelState.IsValid)
            {
            }
            try
            {
                await _landSvc.AddLandProductExpense(
                    User.GetAccountId(),
                    productExpense.ProductId,
                    productExpense.Type,
                    productExpense.Price);

                return LocalRedirect(Routes.Dashboard.Lands.Products.FullPath);
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        public ActionResult Details(long Id)
        {
            var productExpenses = new List<ProductExpenseDto>
            {
                new ProductExpenseDto{Type = "type1", Id = 1, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type2", Id = 2, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type3", Id = 3, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type4", Id = 4, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type5", Id = 5, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type6", Id = 6, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type7", Id = 7, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type8", Id = 8, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type9", Id = 9, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type10", Id = 9, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type11", Id = 9, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
            };

            return View(productExpenses.Where(i => i.Id == Id).First());
        }

        // GET: landProductExpense/Update landProductExpense
        public ActionResult Update(long Id)
        {
            var productExpenses = new List<ProductExpenseDto>
            {
                new ProductExpenseDto{Type = "type1", Id = 1, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type2", Id = 2, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type3", Id = 3, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type4", Id = 4, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type5", Id = 5, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type6", Id = 6, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type7", Id = 7, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type8", Id = 8, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type9", Id = 9, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type10", Id = 9, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type11", Id = 9, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
            };

            return View(productExpenses.Where(i => i.Id == Id).First());
        }

        // POST: landProductExpense/Update landProductExpense
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(ProductExpenseDto productExpense)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: landProductExpense/Delete landProductExpense
        public ActionResult Delete(long Id)
        {
            var productExpenses = new List<ProductExpenseDto>
            {
                new ProductExpenseDto{Type = "type1", Id = 1, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type2", Id = 2, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type3", Id = 3, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type4", Id = 4, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type5", Id = 5, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type6", Id = 6, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type7", Id = 7, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type8", Id = 8, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type9", Id = 9, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type10", Id = 9, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type11", Id = 9, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
            };

            return View(productExpenses.Where(i => i.Id == Id).First());
        }

        // POST: landProductExpense/Delete landProductExpense
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(ProductExpenseDto productExpense)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: landProductExpense/Get landProductExpense
        public ActionResult GetLandProductExpense(long Id)
        {
            var productExpenses = new List<ProductExpenseDto>
            {
                new ProductExpenseDto{Type = "type1", Id = 1, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type2", Id = 2, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type3", Id = 3, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type4", Id = 4, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type5", Id = 5, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type6", Id = 6, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type7", Id = 7, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type8", Id = 8, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type9", Id = 9, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type10", Id = 9, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
                new ProductExpenseDto{Type = "type11", Id = 9, Price = 2134, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now.AddDays(3)},
            };

            return View(productExpenses.Where(i => i.Id == Id).First());
        }
    }
}