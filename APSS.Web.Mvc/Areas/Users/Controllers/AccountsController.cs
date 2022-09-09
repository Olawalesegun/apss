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
        private readonly IUnitOfWork _uow;
        public IEnumerable<AccountDto> accounts;
        private readonly IAccountsService _accountsService;

        public AccountsController(IAccountsService accountsService, IUnitOfWork uow)
        {
            _accountsService = accountsService;
            _uow = uow;
            accounts = new List<AccountDto>
            {
            };
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

        /*   [ApssAuthorized(AccessLevel.Group
               | AccessLevel.District
               | AccessLevel.Village
               | AccessLevel.Presedint
               | AccessLevel.Farmer
               | AccessLevel.Governorate
               | AccessLevel.Directorate
               | AccessLevel.Root, PermissionType.Create)]*/

        public async Task<IActionResult> AddAccount(long id)
        {
            // var account = await _accountsService.GetAccountAsync(User.GetAccountId(), id);

            AccountDto result = new AccountDto();
            result.UserId = id;
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        /*  [ApssAuthorized(AccessLevel.Group
          | AccessLevel.District
          | AccessLevel.Village
          | AccessLevel.Presedint
          | AccessLevel.Farmer
          | AccessLevel.Governorate
          | AccessLevel.Directorate
          | AccessLevel.Root, PermissionType.Create)]*/
        public async Task<IActionResult> AddAccount(AccountDto accountDto)
        {
            var add = await _accountsService.CreateAsync(User.GetAccountId(), accountDto.UserId, accountDto.HolderName, accountDto.PasswordHash, accountDto.PermissionTypeDto.Permissions);

            TempData["Action"] = "Employee Management";
            TempData["success"] = "Add Employee is Successfully";

            return LocalRedirect(Routes.Dashboard.Users.Accounts.FullPath);
        }

        /*     [ApssAuthorized(AccessLevel.Group
              | AccessLevel.District
              | AccessLevel.Village
              | AccessLevel.Presedint
              | AccessLevel.Farmer
              | AccessLevel.Governorate
              | AccessLevel.Directorate
              | AccessLevel.Root, PermissionType.Read)]*/

        public async Task<IActionResult> AccountDetails(long id)
        {
            var account = await _accountsService.GetAccountAsync(User.GetAccountId(), id);
            AccountDto accountDto = new AccountDto
            {
                Id = account.Id,
                HolderName = account.HolderName,
                SocialStatus = (SocialStatusDto)account.SocialStatus,
                IsActive = account.IsActive,
                Job = account.Job,
                NationalId = account.NationalId,
                PhoneNumber = account.PhoneNumber,
                permissionType = account.Permissions,
            };
            accountDto.PermissionTypeDto.Read = accountDto.permissionType.HasFlag(PermissionType.Read);
            accountDto.PermissionTypeDto.Update = accountDto.permissionType.HasFlag(PermissionType.Update);
            accountDto.PermissionTypeDto.Create = accountDto.permissionType.HasFlag(PermissionType.Create);
            accountDto.PermissionTypeDto.Delete = accountDto.permissionType.HasFlag(PermissionType.Delete);
            return View(accountDto);
        }

        [HttpPost]
        public async Task<IActionResult> AdditionalAccountInfo(AccountDto accountDto)
        {
            var account = new AccountDto();
            return View(account);
        }

        /*[ApssAuthorized(AccessLevel.Group
           | AccessLevel.District
           | AccessLevel.Village
           | AccessLevel.Presedint
           | AccessLevel.Farmer
           | AccessLevel.Governorate
           | AccessLevel.Directorate
           | AccessLevel.Root, PermissionType.Delete)]*/

        public async Task<IActionResult> DeleteAccount(long id)
        {
            var account = await _accountsService.GetAccountAsync(User.GetAccountId(), id);
            var accountDto = new AccountDto();
            accountDto.HolderName = account!.HolderName;
            accountDto.Id = account.Id;
            accountDto.IsActive = account.IsActive;
            accountDto.PhoneNumber = account.PhoneNumber;
            accountDto.SocialStatus = (SocialStatusDto)account.SocialStatus;
            accountDto.UserId = 1;
            accountDto.permissionType = account.Permissions;
            accountDto.PermissionTypeDto.Read = accountDto.permissionType.HasFlag(PermissionType.Read);
            accountDto.PermissionTypeDto.Update = accountDto.permissionType.HasFlag(PermissionType.Update);
            accountDto.PermissionTypeDto.Create = accountDto.permissionType.HasFlag(PermissionType.Create);
            accountDto.PermissionTypeDto.Delete = accountDto.permissionType.HasFlag(PermissionType.Delete);
            accountDto.NationalId = account.NationalId;
            accountDto.PasswordHash = account.PasswordHash;
            accountDto.Job = account.Job;
            return View(accountDto);
        }

        /* [ApssAuthorized(AccessLevel.Group
             | AccessLevel.District
             | AccessLevel.Village
             | AccessLevel.Presedint
             | AccessLevel.Farmer
             | AccessLevel.Governorate
             | AccessLevel.Directorate
             | AccessLevel.Root, PermissionType.Delete)]*/

        /*  [ApssAuthorized(AccessLevel.Group
             | AccessLevel.District
             | AccessLevel.Village
             | AccessLevel.Presedint
             | AccessLevel.Farmer
             | AccessLevel.Governorate
             | AccessLevel.Directorate
             | AccessLevel.Root, PermissionType.Update)]*/

        public async Task<IActionResult> EditAccount(long id)
        {
            var account = await _accountsService.GetAccountAsync(User.GetAccountId(), id);
            var accountDto = new AccountDto();
            accountDto.HolderName = account!.HolderName;
            accountDto.Id = account.Id;
            accountDto.IsActive = account.IsActive;
            accountDto.PhoneNumber = account.PhoneNumber;
            accountDto.SocialStatus = (SocialStatusDto)account.SocialStatus;
            accountDto.Job = account.Job;
            accountDto.permissionType = account.Permissions;
            accountDto.PermissionTypeDto.Read = accountDto.permissionType.HasFlag(PermissionType.Read);
            accountDto.PermissionTypeDto.Update = accountDto.permissionType.HasFlag(PermissionType.Update);
            accountDto.PermissionTypeDto.Create = accountDto.permissionType.HasFlag(PermissionType.Create);
            accountDto.PermissionTypeDto.Delete = accountDto.permissionType.HasFlag(PermissionType.Delete);
            accountDto.NationalId = account.NationalId;
            accountDto.PasswordHash = account.PasswordHash;
            return View(accountDto);
        }

        [HttpPost]
        /*      [ApssAuthorized(AccessLevel.Group
                  | AccessLevel.District
                  | AccessLevel.Village
                  | AccessLevel.Presedint
                  | AccessLevel.Farmer
                  | AccessLevel.Governorate
                  | AccessLevel.Directorate
                  | AccessLevel.Root, PermissionType.Update)]*/
        public async Task<IActionResult> EditAccount(AccountDto account)
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

        public IActionResult EditPassword(long id)
        {
            var accountDto = new AccountDto();

            accountDto.Id = id;
            return View(accountDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        /* [ApssAuthorized(AccessLevel.Group
             | AccessLevel.District
             | AccessLevel.Village
             | AccessLevel.Presedint
             | AccessLevel.Farmer
             | AccessLevel.Governorate
             | AccessLevel.Directorate
             | AccessLevel.Root, PermissionType.Update)]*/
        public async Task<IActionResult> EditPassword(AccountDto accountDto)
        {
            var edit = await _accountsService.UpdateAsync(User.GetAccountId(), accountDto.Id, p => p.PasswordHash = accountDto.PasswordHash);
            TempData["Action"] = "Employees";
            TempData["success"] = "Edit Password is  successed";
            return View(accountDto);
        }

        public async Task<IActionResult> test()
        {
            var accounts = await _uow.Accounts.Query().Include(u => u.User).AsAsyncEnumerable().ToListAsync();
            return View(accounts);
        }
    }
}