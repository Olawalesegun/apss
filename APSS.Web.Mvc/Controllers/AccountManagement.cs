using APSS.Web.Dtos;
using Microsoft.AspNetCore.Mvc;
using APSS.Domain.Entities;
using APSS.Domain.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace APSS.Web.Mvc.Controllers
{
    public class AccountManagement : Controller
    {
        private List<UserDto> _userDtos;
        private readonly IAccountsService _accountsService;

        public AccountManagement(IAccountsService accountsService)
        {
            _userDtos = new List<UserDto>
            {
                new UserDto{Name ="user 1",Id=1,CreatedAt=DateTime.Now,AccessLevel=Domain.Entities.AccessLevel.Directorate},
                new UserDto{Name ="user 2",Id=2,CreatedAt=DateTime.Now,AccessLevel=Domain.Entities.AccessLevel.Directorate},
                new UserDto{Name ="user 3",Id=3,CreatedAt=DateTime.Now,AccessLevel=Domain.Entities.AccessLevel.Directorate},
                new UserDto{Name ="user 4",Id=4,CreatedAt=DateTime.Now,AccessLevel=Domain.Entities.AccessLevel.Directorate},
                new UserDto{Name ="user 5",Id=5,CreatedAt=DateTime.Now,AccessLevel=Domain.Entities.AccessLevel.Directorate},
                new UserDto{Name ="user 6",Id=6,CreatedAt=DateTime.Now,AccessLevel=Domain.Entities.AccessLevel.Directorate},
            };
            this._accountsService = accountsService;
        }

        public IActionResult Index()
        {
            List<UserDto> userDto = new List<UserDto>();
            userDto.Add(new UserDto { Name = "user 1", Id = 1, CreatedAt = DateTime.Now, AccessLevel = Domain.Entities.AccessLevel.Directorate });
            userDto.Add(new UserDto { Name = "user 2", Id = 2, CreatedAt = DateTime.Now, AccessLevel = Domain.Entities.AccessLevel.Directorate });
            userDto.Add(new UserDto { Name = "user 3", Id = 3, CreatedAt = DateTime.Now, AccessLevel = Domain.Entities.AccessLevel.Directorate });
            userDto.Add(new UserDto { Name = "user 4", Id = 4, CreatedAt = DateTime.Now, AccessLevel = Domain.Entities.AccessLevel.Directorate });
            userDto.Add(new UserDto { Name = "user 5", Id = 5, CreatedAt = DateTime.Now, AccessLevel = Domain.Entities.AccessLevel.Directorate });
            return View(userDto);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        public async Task<IActionResult> Accounts()
        {
            List<AccountDto> accountDto = new List<AccountDto>
            {
                new AccountDto{Id=1,HolderName="account 1",PhoneNumber="7777777777",NationalId="06754653"},
                new AccountDto{Id=2,HolderName="account 2",PhoneNumber="7777777777",NationalId="06754653"},
                new AccountDto{Id=3,HolderName="account 3",PhoneNumber="7777777777",NationalId="06754653"},
                new AccountDto{Id=4,HolderName="account 4",PhoneNumber="7777777777",NationalId="06754653"},
                new AccountDto{Id=5,HolderName="account 5",PhoneNumber="7777777777",NationalId="06754653"}
            };
            return View(accountDto);
        }

        public async Task<IActionResult> AccountDetails(long id)
        {
            AccountDto accountDto = new AccountDto();
            return View(accountDto);
        }

        public async Task<IActionResult> UserDetials(int id)
        {
            return View();
        }

        public async Task<IActionResult> UsersIndex()
        {
            return View();
        }

        public async Task<IActionResult> AddAccount(long id)
        {
            if (id.Equals(null))
            {
                return View();
            }
            var account = new AccountDto();
            account.UserId = id;

            var perm = new List<SelectListItem>();
            //perm.Add(new SelectListItem { Text = "", Value = "" });

            foreach (PermissionType permission in Enum.GetValues(typeof(PermissionType)))
            {
                perm.Add(new SelectListItem { Text = Enum.GetName(typeof(PermissionType), permission), Value = permission.ToString() });
            }
            var listbox = new List<MultiSelectList>();
            /*  listbox.Add(new MultiSelectList { DataTextField = , })*/
            foreach (PermissionType permission in Enum.GetValues(typeof(PermissionType)))
            {
                /* listbox.Add(
                     new MultiSelectList { }*/
            }
            /* ViewBag.multi=new MultiSelectList(PermissionType,"Id")*/
            List<Enum> permision = new List<Enum>();

            foreach (PermissionType permission in Enum.GetValues(typeof(PermissionType)))
            {
                permision.Add(permission);
            }

            List<perm> p = new List<perm>
            {
                new perm{ Id = 1, Name ="Read"},
                new perm{ Id = 2, Name ="Write"},
                new perm{ Id = 3, Name ="Update"},
                new perm{ Id = 4, Name ="Delete"},
                new perm{ Id = 5, Name ="full"},
            };

            ViewBag.permission = perm;

            return View(account);
        }

        [HttpPost]
        public async Task<IActionResult> AddAccount(AccountDto accountDto)
        {
            /*if (ModelState.IsValid)
            {
                var cockie_accountId = 0;
                var account = await _accountsService.CreateAsync(
                    cockie_accountId,
                    accountDto.UserId,
                    accountDto.HolderName,
                    accountDto.PasswordHash,
                   (Domain.Entities.PermissionType)accountDto.Permissions);
                RedirectToAction(actionName: "Accounts",account);
            }*/
            RedirectToAction("Index");
            return View(accountDto);
        }

        public async Task<IActionResult> AdditionalAccountInfo(long id)
        {
            var account = new AccountDto();
            account.Id = id;
            return View(account);
        }

        [HttpPost]
        public async Task<IActionResult> AdditionalAccountInfo(AccountDto accountDto)
        {
            var account = new AccountDto();
            return View(account);
        }

        public async Task<IActionResult> DeleteAccount(long id)
        {
            var account = new AccountDto();
            return View(account);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmDeleteAccount(AccountDto account)
        {
            RedirectToAction("Index");
            return View();
        }

        public async Task<IActionResult> EditAccount(long id)
        {
            var social = new List<SelectListItem>();
            //perm.Add(new SelectListItem { Text = "", Value = "" });

            foreach (PermissionType socialState in Enum.GetValues(typeof(SocialStatus)))
            {
                social.Add(new SelectListItem { Text = Enum.GetName(typeof(SocialStatus), socialState), Value = socialState.ToString() });
            }
            ViewBag.socialstatus = social;
            var perm = new List<SelectListItem>();
            //perm.Add(new SelectListItem { Text = "", Value = "" });

            foreach (PermissionType permission in Enum.GetValues(typeof(PermissionType)))
            {
                perm.Add(new SelectListItem { Text = Enum.GetName(typeof(PermissionType), permission), Value = permission.ToString() });
            }
            ViewBag.permission = perm;
            var account = new AccountDto();
            return View(account);
        }

        [HttpPost]
        public async Task<IActionResult> EditAccount(AccountDto account)
        {
            Redirect("Index");
            return View(account);
        }

        public async Task<IActionResult> DeleteUser(long id)
        {
            UserDto userDto = new UserDto();
            userDto.Id = 10;
            userDto.Name = "BBB";
            userDto.AccessLevel = Domain.Entities.AccessLevel.Directorate;
            userDto.ModifiedAt = DateTime.Now;
            userDto.CreatedAt = DateTime.Now;
            return View(userDto);
        }

        public async Task<IActionResult> ConfirmDeleteUser(long id)
        {
            if (true)
            {
                RedirectToAction("Index");
            }
            else
            {
                RedirectToAction("DeleteUser");
            }
            RedirectToAction("DeleteUser", id);
            return View();
        }

        public async Task<IActionResult> EditUser(long id)
        {
            UserDto userDto = new UserDto();
            return View(userDto);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(UserDto userDto)
        {
            return View(userDto);
        }

        public async Task<IActionResult> UserAccounts(long id)
        {
            var accountDto = new List<AccountDto>();
            return View(accountDto);
        }
    }
}

internal class perm
{
    public int Id { get; set; }
    public string Name { get; set; }
}