using APSS.Web.Dtos;
using Microsoft.AspNetCore.Mvc;
using APSS.Domain.Entities;
using APSS.Domain.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using CustomClaims = APSS.Web.Mvc.Auth.CustomClaims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using APSS.Web.Mvc.Auth;

namespace APSS.Web.Mvc.Controllers
{
    public class Users : Controller
    {
        private List<UserDto> _userDtos;
        private readonly IUsersService _userService;

        public Users(IUsersService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            List<UserDto> userDto = new List<UserDto>();
            var user = await (await _userService.GetSubuserAsync(1)).AsAsyncEnumerable().ToListAsync();
            foreach (var userDtoItem in user)
            {
                userDto.Add(new UserDto
                {
                    Id = userDtoItem.Id,
                    Name = userDtoItem.Name,
                    AccessLevel = userDtoItem.AccessLevel,
                    userStatus = userDtoItem.UserStatus,
                    CreatedAt = userDtoItem.CreatedAt,
                });
            }
            return View(userDto);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string searchString, string searchBy)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.Trim();
                var result = new List<UserDto>();
                if (searchBy == "2")
                {
                    result = _userDtos.Where(u => u.Id == Convert.ToInt64(searchString)).ToList();
                    return View(result);
                }
                else
                {
                    result = _userDtos.Where(u => u.Name.Contains(searchString)).ToList();
                }
            }
            var user = new List<User>();
            return View(user);
        }

        public async Task<IActionResult> AddUser()
        {
            var userDto = new UserDto();
            ViewBag.status = userDto.userStatus;
            return View(userDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUser(UserDto user)
        {
            var accountis = 1;
            var add = await _userService.CreateAsync(accountis, user.Name);
            return RedirectToAction("Index");

            return View(user);
        }

        public async Task<IActionResult> Accounts()
        {
            List<AccountDto> accountDto = new List<AccountDto>();
            return View(accountDto);
        }

        public async Task<IActionResult> UserDetials(int id)
        {
            var user = _userDtos.Where(m => m.Id == id).FirstOrDefault();
            return View(user);
        }

        public async Task<IActionResult> UsersIndex()
        {
            return View();
        }

        public async Task<IActionResult> DeleteUser(long id)
        {
            UserDto userDto = new UserDto();
            return View(userDto);
        }

        public async Task<IActionResult> ConfirmDeleteUser(long id)
        {
            return RedirectToAction("Index");
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
    }
}