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
            var expenses = await (
                await _landSvc.GetLandProductExpensesAsync(User.GetAccountId(), Id))
                .AsAsyncEnumerable()
                .ToListAsync();

            return View(expenses.Select(_mapper.Map<ProductExpenseDto>));
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
        public async Task<IActionResult> Add(ProductExpenseDto productExpense)
        {
            if (!ModelState.IsValid)
            {
            }
            await _landSvc.AddLandProductExpense(
                User.GetAccountId(),
                productExpense.ProductId,
                productExpense.Type,
                productExpense.Price);

            TempData["success"] = "Expense Added successfully";

            return LocalRedirect(Routes.Dashboard.Lands.Products.FullPath);
        }

        // GET: landProductExpense/Update landProductExpense
        //[ApssAuthorized(AccessLevel.Farmer, PermissionType.Update)]
        public async Task<IActionResult> Update(long Id)
        {
            return View(_mapper.Map<ProductExpenseDto>(
                await (await _landSvc.GetLandProductExpenseAsync(User.GetAccountId(), Id)).FirstAsync()));
        }

        // POST: landProductExpense/Update landProductExpense
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[ApssAuthorized(AccessLevel.Farmer, PermissionType.Update)]
        public async Task<IActionResult> Update(ProductExpenseDto productExpense)
        {
            if (!ModelState.IsValid)
            { }

            await _landSvc.UpdateLandProductExpenseAsync(
                User.GetAccountId(),
                productExpense.Id,
                f =>
                {
                    f.Type = productExpense.Type;
                    f.Price = productExpense.Price;
                });

            TempData["success"] = "Expense Updated successfully";

            return LocalRedirect(Routes.Dashboard.Lands.Products.FullPath);
        }

        // GET: landProductExpense/Delete landProductExpense
        //[ApssAuthorized(AccessLevel.Farmer, PermissionType.Delete)]
        public async Task<IActionResult> Delete(long Id)
        {
            return View(_mapper.Map<ProductExpenseDto>(
                await (await _landSvc.GetLandProductExpenseAsync(User.GetAccountId(), Id)).FirstAsync()));
        }

        //[ApssAuthorized(AccessLevel.Farmer, PermissionType.Delete)]
        public async Task<IActionResult> DeletePost(long id)
        {
            await _landSvc.RemoveLandProductExpenseAsync(User.GetAccountId(), id);

            TempData["success"] = "Expense Removed Successfully";

            return LocalRedirect(Routes.Dashboard.Lands.Products.FullPath);
        }

        // GET: landProductExpense/Get landProductExpense
        public async Task<IActionResult> GetExpense(long Id)
        {
            return View(_mapper.Map<ProductExpenseDto>(
                await (await _landSvc.GetLandProductExpenseAsync(User.GetAccountId(), Id)).FirstAsync()));
        }
    }
}