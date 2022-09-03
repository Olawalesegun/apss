using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using APSS.Domain.Entities;
using APSS.Domain.Repositories;
using APSS.Domain.Services;
using APSS.Web.Dtos;
using APSS.Web.Mvc.Auth;
using CustomClaims = APSS.Web.Mvc.Auth.CustomClaims;

namespace APSS.Web.Mvc.Controllers
{
    public class UsersController : Controller
    {
        private List<UserDto> _userDtos;
        private readonly IUsersService _userService;
        private readonly IUnitOfWork _uow;

        public UsersController(IUsersService userService)
        {
            _userService = userService;
            //_uow = uow;
        }

        public async Task<IActionResult> Index()
        {
            List<UserDto> userDto = new List<UserDto>();
            var user = await (await _userService.GetSubuserAsync(1)).AsAsyncEnumerable().ToListAsync();
            var users = await _uow.Users.Query().FirstAsync(u => u.Id == 1);
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
            userDto.Add(new UserDto { Name = users.Name, Id = users.Id, AccessLevel = users.AccessLevel, userStatus = users.UserStatus, CreatedAt = users.CreatedAt });
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
            try
            {
                var accountis = 1;
                var add = await _userService.CreateAsync(accountis, user.Name);
                return RedirectToAction("Index");
            }
            catch (Exception) { }
            return View(user);
        }

        public async Task<IActionResult> Accounts()
        {
            List<AccountDto> accountDto = new List<AccountDto>();
            return View(accountDto);
        }

        public async Task<IActionResult> UserDetials(int id)
        {
            var users = await _uow.Users.Query().FirstAsync(u => u.Id == 1);
            if (users == null) return NotFound();
            var userDto = new UserDto
            {
                Name = users.Name,
                Id = users.Id,
                AccessLevel = users.AccessLevel,
                userStatus = users.UserStatus,
                CreatedAt = users.CreatedAt,
            };
            return View(userDto);
        }

        public async Task<IActionResult> DeleteUser(long id)
        {
            UserDto userDto = new UserDto();
            return View(userDto);
        }

        public async Task<IActionResult> ConfirmDeleteUser(long id)
        {
            try
            {
                var user = await _uow.Users.Query().FirstAsync(u => u.Id == id);
                if (user == null) return NotFound();
                TempData["Action"] = "Delete Erea";
                TempData["success"] = "Erea Deleted Successfully";
                return RedirectToAction("Index");
            }
            catch (Exception) { }
            var userDto = new UserDto();
            return View(userDto);
        }

        public async Task<IActionResult> EditUser(long id)
        {
            UserDto userDto = new UserDto();
            return View(userDto);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(UserDto userDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(userDto);
                }
                else
                {
                    var edit = await _userService.UpdateAsync(1, 1, p =>
                        {
                            p.Name = userDto.Name;
                            p.UserStatus = userDto.userStatus;
                        });
                    if (edit == null)
                    {
                        TempData["Action"] = "Update Erea";
                        TempData["success"] = "Update Failed!!!";
                    }
                    else
                    {
                        TempData["Action"] = "Update Erea";
                        TempData["success"] = "Erea Updated Successfully";
                    }

                    return RedirectToAction("Index");
                }
            }
            catch (Exception) { }
            return View(userDto);
        }
    }
}