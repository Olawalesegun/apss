﻿using Microsoft.AspNetCore.Mvc;
using APSS.Domain.Entities;
using APSS.Domain.Repositories;
using APSS.Domain.Services;
using APSS.Web.Dtos;
using APSS.Web.Mvc.Auth;
using APSS.Web.Dtos.ValueTypes;

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
            try
            {
                var userAccount = await _uow.Accounts.Query().Include(u => u.User).Where(i => i.Id == (long)User.GetId()).FirstAsync();
                var accountsObject = await (await _accountsService.GetUserAccounts(User.GetId(), userAccount.User.Id)).AsAsyncEnumerable().ToListAsync();
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
            catch (Exception)
            {
                return RedirectToAction(nameof(Index), "Home");
            }
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

        public IActionResult AddAccount(long id)
        {
            try
            {
                AccountDto result = new AccountDto();
                result.UserId = id;
                return View(result);
            }
            catch (Exception)
            {
            }
            return RedirectToAction("Index", "User");
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
            try
            {
                if (accountDto == null)
                    return View(accountDto);
                else
                {
                    var add = await _accountsService.CreateAsync(User.GetId(), accountDto.UserId, accountDto.HolderName, accountDto.PasswordHash, accountDto.PermissionTypeDto.Permissions);
                    if (add != null)
                    {
                        TempData["Action"] = "Employee Management";
                        TempData["success"] = "Add Employee is Successfully";
                    }
                    else
                    {
                        TempData["Action"] = "Employee Management";
                        TempData["success"] = "Add Failed!!!";
                    }
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
            }

            return View(accountDto);
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
            var account = await _accountsService.GetAccountAsync(User.GetId(), id);
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
            if (id > 0)
            {
                var account = await _accountsService.GetAccountAsync(User.GetId(), id);
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
            return RedirectToAction("Index");
        }

        /* [ApssAuthorized(AccessLevel.Group
             | AccessLevel.District
             | AccessLevel.Village
             | AccessLevel.Presedint
             | AccessLevel.Farmer
             | AccessLevel.Governorate
             | AccessLevel.Directorate
             | AccessLevel.Root, PermissionType.Delete)]*/

        public async Task<IActionResult> ConfirmDeleteAccount(long id)
        {
            try
            {
                if (id > 0)
                {
                    await _accountsService.RemoveAsync(User.GetId(), id);
                    TempData["Action"] = "الموظفين";
                    TempData["success"] = "تم حذف الموظف بنجاح";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception) { }
            return RedirectToAction(nameof(Index));
        }

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
            try
            {
                if (id > 0)
                {
                    var account = await _accountsService.GetAccountAsync(User.GetId(), id);
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
            }
            catch (Exception) { }
            return RedirectToAction("Index");
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
            try
            {
                var resultEdit = await _accountsService.UpdateAsync(User.GetId(), account.Id, p =>
                    {
                        p.HolderName = account.HolderName;
                        p.Job = account.Job;
                        p.NationalId = account.NationalId;
                        p.PhoneNumber = account.PhoneNumber;
                        p.SocialStatus = (SocialStatus)account.SocialStatus;
                        p.Permissions = account.PermissionTypeDto.Permissions;
                        p.IsActive = account.IsActive;
                    });
                if (resultEdit != null)
                {
                    TempData["Action"] = "Employees";
                    TempData["success"] = "Edit Employee is successfully";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Action"] = "Employees";
                    TempData["success"] = "Edit Employee is not Successed";
                    return View(account);
                }
            }
            catch (Exception) { }

            return View();
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
            var entityAccount3 = await (await _accountsService.GetUserAccounts(User.GetId(), id)).AsAsyncEnumerable().ToListAsync();
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

        public async Task<IActionResult> EditPassword(long id)
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
            try
            {
                if (accountDto.PasswordHash == null)
                {
                    return View(accountDto);
                }
                else
                {
                    var edit = await _accountsService.UpdateAsync(1, 1, p => p.PasswordHash = accountDto.PasswordHash);
                    if (edit != null)
                    {
                        TempData["Action"] = "Employees";
                        TempData["success"] = "Edit Password is  successfully";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["Action"] = "Employees";
                        TempData["success"] = "Edit Password is not successed";
                        return View(accountDto);
                    }
                }
            }
            catch (Exception) { }
            return View(accountDto);
        }

        public async Task<IActionResult> test()
        {
            var accounts = await _uow.Accounts.Query().Include(u => u.User).AsAsyncEnumerable().ToListAsync();

            return View(accounts);
        }
    }
}