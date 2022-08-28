using APSS.Web.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace APSS.Web.Mvc.Controllers
{
    public class Accounts : Controller
    {
        public IEnumerable<AccountDto> accounts;

        public Accounts()
        {
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

        public IActionResult Index()
        {
            var account = accounts;
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
            if (id.Equals(null))
            {
                return View();
            }
            var account = new AccountDto();
            account.UserId = id;

            var perm = new List<SelectListItem>();
            perm.Add(new SelectListItem { Text = "اختار الصلاحيات", Value = "" });

            foreach (PermissionType permission in Enum.GetValues(typeof(PermissionType)))
            {
                perm.Add(new SelectListItem { Text = Enum.GetName(typeof(PermissionType), permission), Value = permission.ToString() });
            }
            var listbox = new List<MultiSelectList>();

            List<Enum> permision = new List<Enum>();

            foreach (PermissionType permission in Enum.GetValues(typeof(PermissionType)))
            {
                permision.Add(permission);
            }

            ViewBag.permission = perm;

            return View(account);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAccount(AccountDto accountDto)
        {
            TempData["Action"] = "الموظفـين";
            TempData["success"] = "تم اضافة الموظـف بنجاح";
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("AddAccount");
            /*return View(accountDto);*/
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
            var account = new AccountDto();
            return View(account);
        }

        public async Task<IActionResult> ConfirmDeleteAccount(long id)
        {
            TempData["Action"] = "الموظفين";
            TempData["success"] = "تم حذف الموظف بنجاح";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EditAccount(long id)
        {
            var social = new List<SelectListItem>();

            foreach (PermissionType socialState in Enum.GetValues(typeof(SocialStatus)))
            {
                social.Add(new SelectListItem { Text = Enum.GetName(typeof(SocialStatus), socialState), Value = socialState.ToString() });
            }
            ViewBag.socialstatus = social;
            var perm = new List<SelectListItem>();

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
            TempData["Action"] = "الموظفين";
            TempData["success"] = "تم تعديل الموظف بنجاح";

            return RedirectToAction("Index");
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