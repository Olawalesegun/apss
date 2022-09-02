using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using APSS.Domain.Services;
using APSS.Web.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using CustomClaims = APSS.Web.Mvc.Auth.CustomClaims;
using Microsoft.AspNetCore.Authentication;
using APSS.Web.Mvc.Auth;
using APSS.Application.App;
using APSS.Domain.Repositories;
using APSS.Domain.Entities;

namespace APSS.Web.Mvc.Controllers
{
    public class Accounts : Controller
    {
        private readonly IUnitOfWork _uow;
        public IEnumerable<AccountDto> accounts;
        private readonly IAccountsService _accountsService;

        public Accounts(IAccountsService accountsService, IUnitOfWork uow)
        {
            _accountsService = accountsService;
            _uow = uow;
            accounts = new List<AccountDto> {
            new AccountDto{Id = 1, HolderName="account 1",NationalId="1244551",PhoneNumber="7657879876",CreatedAt=DateTime.Now},
            new AccountDto{Id = 2, HolderName="one 1",NationalId="1244551",PhoneNumber="765779876",CreatedAt=DateTime.Now},
            new AccountDto{Id = 3, HolderName="one 2",NationalId="1244551",PhoneNumber="76578876",CreatedAt=DateTime.Now},
            new AccountDto{Id = 4, HolderName="two 1",NationalId="1244551",PhoneNumber="76578799876",CreatedAt=DateTime.Now},
            new AccountDto{Id = 5, HolderName="tow 2",NationalId="1244551",PhoneNumber="76578791006",CreatedAt=DateTime.Now},
            new AccountDto{Id = 6, HolderName="account 3",NationalId="1244551",PhoneNumber="76557879876",CreatedAt=DateTime.Now},
            new AccountDto{Id = 7, HolderName="account 2",NationalId="1244551",PhoneNumber="76657879876",CreatedAt=DateTime.Now},
            };
        }

        public async Task<IActionResult> Index()
        {
            var entityAccount = await _uow.Accounts.Query().FirstAsync();
            var entityAccount2 = await _uow.Accounts.Query().FirstAsync(i => i.Id == 6);
            var account = new List<AccountDto>();
            account.Add(new AccountDto
            {
                HolderName = entityAccount.HolderName,
                Id = entityAccount.Id,
                PhoneNumber = entityAccount.PhoneNumber,
                NationalId = entityAccount.NationalId,
                IsActive = entityAccount.IsActive,
                Job = entityAccount.Job
            });
            account.Add(new AccountDto
            {
                HolderName = entityAccount2.HolderName,
                Id = entityAccount2.Id,
                PhoneNumber = entityAccount2.PhoneNumber,
                NationalId = entityAccount2.NationalId,
                IsActive = entityAccount2.IsActive,
                Job = entityAccount2.Job
            });
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

        public async Task<IActionResult> AddAccount(long id)
        {
            var account = await _uow.Accounts.Query().FirstAsync();
            /*if (id.Equals(null))
            {
                return View();
            }*/
            /*var account = new AccountDto();
            account.UserId = id;*/
            AccountDto result = new AccountDto
            {
                Id = account.Id,
                HolderName = account.HolderName,
                PhoneNumber = account.PhoneNumber,
                NationalId = account.NationalId,
            };
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAccount(AccountDto accountDto)
        {
            var accountId = 1;
            var userID = 1;
            try
            {
                var add = await _accountsService.CreateAsync(accountId, userID, accountDto.HolderName, accountDto.PasswordHash, accountDto.PermissionTypeDto.Permissions);
                TempData["Action"] = "الموظفـين";
                TempData["success"] = "تم اضافة الموظـف بنجاح";
                return RedirectToAction("IndeX");
            }
            catch (Exception)
            {
            }

            return View(accountDto);
        }

        public async Task<IActionResult> AdditionalAccountInfo(long id)
        {
            var account = new AccountDto();
            account.Id = id;
            return View(account);
        }

        public async Task<IActionResult> AccountDetails(long id)
        {
            AccountDto accountDto = new AccountDto();
            accountDto.User = new UserDto();
            return View(accountDto);
        }

        [HttpPost]
        public async Task<IActionResult> AdditionalAccountInfo(AccountDto accountDto)
        {
            var account = new AccountDto();
            return View(account);
        }

        public async Task<IActionResult> DeleteAccount(long id)
        {
            if (id > 0)
            {
                var account = await _uow.Accounts.Query().FirstOrNullAsync(i => i.Id == id);
                var accountDto = new AccountDto();
                accountDto.HolderName = account!.HolderName;
                accountDto.Id = account.Id;
                accountDto.IsActive = account.IsActive;
                accountDto.PhoneNumber = account.PhoneNumber;
                accountDto.SocialStatus = account.SocialStatus;
                accountDto.UserId = 1;
                accountDto.permissionType = account.Permissions;
                accountDto.PermissionTypeDto.Read = accountDto.permissionType.HasFlag(PermissionType.Read);
                accountDto.PermissionTypeDto.Update = accountDto.permissionType.HasFlag(PermissionType.Update);
                accountDto.PermissionTypeDto.Create = accountDto.permissionType.HasFlag(PermissionType.Create);
                accountDto.PermissionTypeDto.Delete = accountDto.permissionType.HasFlag(PermissionType.Delete);
                accountDto.NationalId = account.NationalId;
                accountDto.PasswordHash = account.PasswordHash;
                return View(accountDto);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ConfirmDeleteAccount(long id)
        {
            if (id > 0)
            {
                await _accountsService.RemoveAsync(User.GetId(), id);
                TempData["Action"] = "الموظفين";
                TempData["success"] = "تم حذف الموظف بنجاح";
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EditAccount(long id)
        {
            if (id > 0)
            {
                var account = await _uow.Accounts.Query().FirstOrNullAsync(i => i.Id == id);
                var accountDto = new AccountDto();
                accountDto.HolderName = account.HolderName;
                accountDto.Id = account.Id;
                accountDto.IsActive = account.IsActive;
                accountDto.PhoneNumber = account.PhoneNumber;
                accountDto.SocialStatus = account.SocialStatus;
                accountDto.UserId = 1;
                accountDto.permissionType = account.Permissions;
                accountDto.PermissionTypeDto.Read = accountDto.permissionType.HasFlag(PermissionType.Read);
                accountDto.PermissionTypeDto.Update = accountDto.permissionType.HasFlag(PermissionType.Update);
                accountDto.PermissionTypeDto.Create = accountDto.permissionType.HasFlag(PermissionType.Create);
                accountDto.PermissionTypeDto.Delete = accountDto.permissionType.HasFlag(PermissionType.Delete);
                accountDto.NationalId = account.NationalId;
                accountDto.PasswordHash = account.PasswordHash;
                return View(accountDto);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> EditAccount(AccountDto account)
        {
            try
            {
                var resultEdit = await _accountsService.UpdateAsync(1, 1, p =>
                  {
                      p.HolderName = account.HolderName;
                      p.Job = account.Job;
                      p.NationalId = account.NationalId;
                      p.PhoneNumber = account.PhoneNumber;
                      p.SocialStatus = account.SocialStatus;
                      p.PasswordHash = account.PasswordHash;
                      p.Permissions = account.PermissionTypeDto.Permissions;
                      p.IsActive = account.IsActive;
                  });
                TempData["Action"] = "Employees";
                TempData["success"] = "Edit Employee is succesfully";
                return RedirectToAction("Index");
            }
            catch (Exception) { }

            return View();
        }

        public async Task<IActionResult> UserAccounts(long id)
        {
            var account = new List<AccountDto>();
            account.Add(new AccountDto { HolderName = "محمد", Id = 1, NationalId = "1212", PhoneNumber = "7777777777", CreatedAt = new DateTime().AddDays(30) });
            account.Add(new AccountDto { HolderName = "محمد", Id = 1, NationalId = "1212", PhoneNumber = "7777777777", CreatedAt = new DateTime().AddDays(30) });
            account.Add(new AccountDto { HolderName = "محمد", Id = 1, NationalId = "1212", PhoneNumber = "7777777777", CreatedAt = new DateTime().AddDays(30) });

            return View(account);
        }
    }
}

public class Pager
{
    public int TotalItem { get; private set; }
    public int CorrentPage { get; private set; }
    public int PagrSize { get; private set; }
    public int TotalPages { get; set; }
    public int StartPage { get; set; }
    public int EndPage { get; set; }

    public Pager()
    {
    }

    public Pager(int totalItem, int page, int pageSize = 10)
    {
        int totalPage = (int)System.Math.Ceiling((decimal)totalItem / (decimal)pageSize);
        int correntPage = page;

        int startPage = correntPage - 5;
        int endPage = correntPage + 4;

        if (startPage < 0)
        {
            endPage = endPage - (startPage - 1);
            startPage = 1;
        }
        if (endPage > totalPage)
        {
            endPage = totalPage;
            if (endPage > pageSize)
            {
                startPage = endPage - 9;
            }
        }

        TotalItem = totalItem;
        CorrentPage = correntPage;
        PagrSize = pageSize;
        StartPage = startPage;
        EndPage = endPage;
        TotalPages = totalPage;
    }
}