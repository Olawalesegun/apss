using APSS.Web.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace APSS.Web.Mvc.Controllers
{
    public class Accounts : Controller
    {
        public IActionResult Index()
        {
            TempData["Action"] = "Accounts";
            TempData["success"] = "success";

            var account = new List<AccountDto>();
            account.Add(new AccountDto { HolderName = "محمد", Id = 1, NationalId = "1212", PhoneNumber = "7777777777", CreatedAt = new DateTime().AddDays(30) });
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
        public async Task<IActionResult> AddAccount(AccountDto accountDto)
        {
            return RedirectToAction("Index");
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
            Redirect("Index");
            return View(account);
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