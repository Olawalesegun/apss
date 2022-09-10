using Microsoft.AspNetCore.Mvc;
using APSS.Domain.Entities;
using APSS.Domain.Repositories;
using APSS.Domain.Services;
using APSS.Web.Dtos;
using APSS.Web.Mvc.Auth;
using APSS.Web.Dtos.ValueTypes;
using APSS.Web.Mvc.Util.Navigation.Routes;

namespace APSS.Web.Mvc.Areas.Controllers
{
    [Area(Areas.Users)]
    public class AccountsController : Controller
    {
        private readonly IAccountsService _accountsService;

    public AccountsController(IAccountsService accountsService, IMapper mapper)
        {
            _accountsService = accountsService;
        _mapper = mapper;
        }

        /* [ApssAuthorized(AccessLevel.Group
             | AccessLevel.District
             | AccessLevel.Village
             | AccessLevel.Presedint
             | AccessLevel.Farmer
             | AccessLevel.Governorate
             | AccessLevel.Directorate
             | AccessLevel.Root, PermissionType.Read)]*/

        public async Task<IActionResult> Index()
        {
            var accountsObject = await (await _accountsService.GetUserAccounts(User.GetAccountId(), User.GetUserId())).AsAsyncEnumerable().ToListAsync();
            var account = new List<AccountDto>();
            foreach (var accountDto in accountsObject)
            {
                account.Add(new AccountDto
                {
                    HolderName = accountDto.HolderName,
                    Id = accountDto.Id,
                    PhoneNumber = accountDto.PhoneNumber,
                    NationalId = accountDto.NationalId,
                    IsActive = accountDto.IsActive,
                    Job = accountDto.Job
                });
            }

            return View(account);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string searchString, string searchBy)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.Trim();
                var result = new List<AccountDto>();
                if (searchBy == "2")
                {
                    result = accounts.Where(a => a.Id == Convert.ToInt64(searchString)).ToList();
                    return View(result);
                }
                else if (searchBy == "3")
                {
                    result = accounts.Where(phone => phone.PhoneNumber.Contains(searchString)).ToList();
                    return View(result);
                }
                else if (searchBy == "4")
                {
                    result = accounts.Where(a => a.NationalId.Contains(searchString)).ToList();
                    return View(result);
                }
                else
                {
                    result = accounts.Where(a => a.HolderName.Contains(searchString)).ToList();
                    return View(result);
                }
            }
            var account = new List<AccountDto>();
            return View(account);
        }

        /*     [ApssAuthorized(AccessLevel.Group
              | AccessLevel.District
              | AccessLevel.Village
              | AccessLevel.Presedint
              | AccessLevel.Farmer
              | AccessLevel.Governorate
              | AccessLevel.Directorate
              | AccessLevel.Root, PermissionType.Read)]*/

        [HttpGet]
    public IActionResult Add(long id)
        => View(new AddAccountForm { UserId = id });

        [HttpPost]
        [ApssAuthorized(AccessLevel.Group
                  | AccessLevel.District
                  | AccessLevel.Village
                  | AccessLevel.Presedint
                  | AccessLevel.Farmer
                  | AccessLevel.Governorate
                  | AccessLevel.Directorate
                  | AccessLevel.Root, PermissionType.Update)]
        public async Task<IActionResult> Update(AccountDto account)
        {
            var resultEdit = await _accountsService.UpdateAsync(User.GetAccountId(), account.Id, p =>
                {
                    p.HolderName = account.HolderName;
                    p.Job = account.Job;
                    p.NationalId = account.NationalId;
                    p.PhoneNumber = account.PhoneNumber;
                    p.SocialStatus = (SocialStatus)account.SocialStatus;
                    p.Permissions = account.PermissionTypeDto.Permissions;
                    p.IsActive = account.IsActive;
                });

            TempData["Action"] = "Employees";
            TempData["success"] = "Edit Employee is successfully";
            return View(account);
        }

        /* [ApssAuthorized(AccessLevel.Group
            | AccessLevel.District
            | AccessLevel.Village
            | AccessLevel.Presedint
            | AccessLevel.Farmer
            | AccessLevel.Governorate
            | AccessLevel.Directorate
            | AccessLevel.Root, PermissionType.Read)]*/

        public async Task<IActionResult> UserAccounts(long id)
        {
            var entityAccount3 = await (await _accountsService.GetUserAccounts(User.GetAccountId(), id)).AsAsyncEnumerable().ToListAsync();
            var account = new List<AccountDto>();
            foreach (var accountDto in entityAccount3)
            {
                account.Add(new AccountDto
                {
                    HolderName = accountDto.HolderName,
                    Id = accountDto.Id,
                    PhoneNumber = accountDto.PhoneNumber,
                    NationalId = accountDto.NationalId,
                    IsActive = accountDto.IsActive,
                    Job = accountDto.Job
                });
            }
            return View(account);
        }

        /* [ApssAuthorized(AccessLevel.Group
            | AccessLevel.District
            | AccessLevel.Village
            | AccessLevel.Presedint
            | AccessLevel.Farmer
            | AccessLevel.Governorate
            | AccessLevel.Directorate
            | AccessLevel.Root, PermissionType.Update)]*/

        public IActionResult UpdatePassword(long id)
        {
            var accountDto = new AccountDto();

            accountDto.Id = id;
            return View(accountDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(AddAccountForm form)
        {
        var _ = await _accountsService.CreateAsync(
            User.GetAccountId(),
            form.UserId,
            form.HolderName,
            form.Password,
            form.Permissions.Permissions);

        return LocalRedirect(Routes.Dashboard.Users.Accounts.FullPath + $"?id={form.UserId}");
    }
}