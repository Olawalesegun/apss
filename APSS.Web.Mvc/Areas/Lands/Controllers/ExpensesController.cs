using Microsoft.AspNetCore.Mvc;
using APSS.Domain.Entities;
using APSS.Domain.Services;
using APSS.Domain.ValueTypes;
using APSS.Web.Dtos;
using APSS.Web.Dtos.Forms;
using APSS.Web.Mvc.Auth;
using APSS.Web.Mvc.Util.Navigation.Routes;
using AutoMapper;
using APSS.Web.Dtos.Parameters;
using APSS.Web.Mvc.Models;

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

        [ApssAuthorized(AccessLevel.Farmer, PermissionType.Read)]
        public async Task<IActionResult> Index([FromQuery] FilteringParameters args)
        {
            var expenses = await (
                await _landSvc.GetExpensesByUserAsync(User.GetAccountId(), User.GetUserId()))
                .Where(u => u.Type.Contains(args.Query ?? string.Empty))
                .Page(args.Page, args.PageLength)
                .AsAsyncEnumerable()
                .Select(_mapper.Map<ProductExpenseDto>)
                .ToListAsync();

            return View(new CrudViewModel<ProductExpenseDto>(expenses, args));
        }

        [HttpGet]
        [ApssAuthorized(AccessLevel.Farmer, PermissionType.Create)]
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
        [ApssAuthorized(AccessLevel.Farmer, PermissionType.Create)]
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

            return LocalRedirect(Routes.Dashboard.Lands.Expenses.FullPath);
        }

        // GET: landProductExpense/Update landProductExpense
        [ApssAuthorized(AccessLevel.Farmer, PermissionType.Update)]
        public async Task<IActionResult> Update(long Id)
        {
            return View(_mapper.Map<ProductExpenseDto>(
                await (await _landSvc.GetLandProductExpenseAsync(User.GetAccountId(), Id))
                             .FirstAsync()));
        }

        // POST: landProductExpense/Update landProductExpense
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ApssAuthorized(AccessLevel.Farmer, PermissionType.Update)]
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

            return LocalRedirect(Routes.Dashboard.Lands.Expenses.FullPath);
        }

        // GET: landProductExpense/Delete landProductExpense
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ApssAuthorized(AccessLevel.Farmer, PermissionType.Delete)]
        public async Task<IActionResult> Delete(long Id)
        {
            await _landSvc.RemoveLandProductExpenseAsync(User.GetAccountId(), Id);

            TempData["success"] = "Expense Removed Successfully";

            return LocalRedirect(Routes.Dashboard.Lands.Expenses.FullPath);
        }

        // GET: landProductExpense/Get landProductExpense
        [ApssAuthorized(AccessLevel.Farmer, PermissionType.Read)]
        public async Task<IActionResult> byLand([FromQuery] FilteringParameters args, long Id)
        {
            var expenses = await (
                await _landSvc.GetExpensesByProductAsync(User.GetAccountId(), Id))
                .Where(u => u.Type.Contains(args.Query ?? String.Empty))
                .Page(args.Page, args.PageLength)
                .AsAsyncEnumerable()
                .Select(_mapper.Map<ProductExpenseDto>)
                .ToListAsync();

            return View(new CrudViewModel<ProductExpenseDto>(expenses, args));
        }

        [ApssAuthorized(AccessLevel.All, PermissionType.Read)]
        public async Task<IActionResult> byUser([FromQuery] FilteringParameters args, long Id)
        {
            var expenses = await (
                await _landSvc.GetExpensesByUserAsync(User.GetAccountId(), Id))
                .Where(u => u.Type.Contains(args.Query ?? String.Empty))
                .Page(args.Page, args.PageLength)
                .AsAsyncEnumerable()
                .Select(_mapper.Map<ProductExpenseDto>)
                .ToListAsync();

            return View(new CrudViewModel<ProductExpenseDto>(expenses, args));
        }
    }
}