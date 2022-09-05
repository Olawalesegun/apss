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

namespace APSS.Web.Mvc.Areas.Controllers
{
    [Area(Areas.Users)]
    public class UsersController : Controller
    {
        private List<UserDto> _userDtos;
        private readonly IUsersService _userService;
        private readonly IUnitOfWork _uow;

        public UsersController(IUsersService userService, IUnitOfWork uow)
        {
            _userService = userService;
            _uow = uow;
        }

        public async Task<IActionResult> Index()
        {
            List<UserDto> userDto = new List<UserDto>();
            var user = await (await _userService.GetSubuserAsync((int)User.GetAccountId())).AsAsyncEnumerable().ToListAsync();
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
            try
            {
                if (!ModelState.IsValid)
                {
                    var add = await _userService.CreateAsync(User.GetAccountId(), user.Name);
                    TempData["Action"] = "Add Erea";
                    TempData["success"] = $"{user.Name} is addedd successfully";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception)
            {
                return Problem("Some Thing error");
            }
            return View(user);
        }

        public async Task<IActionResult> Accounts()
        {
            List<AccountDto> accountDto = new List<AccountDto>();
            return View(accountDto);
        }

        public async Task<IActionResult> UserDetials(int id)
        {
            var user = await (await _userService.GetUserAsync(1, User.GetAccountId())).AsAsyncEnumerable().ToListAsync();
            if (user == null) return NotFound();
            var users = user.FirstOrDefault();
            var userDto = new UserDto
            {
                Name = users!.Name,
                Id = users.Id,
                AccessLevel = users.AccessLevel,
                userStatus = users.UserStatus,
                CreatedAt = users.CreatedAt,
                ModifiedAt = users.ModifiedAt
            };
            return View(userDto);
        }

        public async Task<IActionResult> DeleteUser(long id)
        {
            try
            {
                if (id > 0)
                {
                    var user = await (await _userService.GetUserAsync(1, id)).AsAsyncEnumerable().ToListAsync();
                    if (user == null) return RedirectToAction(nameof(Index));
                    var users = user.FirstOrDefault();
                    var userDto = new UserDto
                    {
                        Name = users!.Name,
                        Id = users.Id,
                        AccessLevel = users.AccessLevel,
                        userStatus = users.UserStatus,
                        CreatedAt = users.CreatedAt,
                    };
                    return View(userDto);
                }
                else
                {
                    TempData["Action"] = "Delete Erea";
                    TempData["success"] = "Erea Id Is Null!!!";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ConfirmDeleteUser(long id)
        {
            try
            {
                if (id > 0)
                {
                    var user = await (await _userService.GetUserAsync(1, id)).AsAsyncEnumerable().ToListAsync();
                    if (user == null) return NotFound();
                    var delete = await _userService.SetUserStatusAsync(1, id, UserStatus.Terminated);
                    if (delete == null) return RedirectToAction(nameof(Index));
                    TempData["Action"] = "Delete Erea";
                    TempData["success"] = "Erea Deleted Successfully";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["Action"] = "Delete Erea";
                    TempData["success"] = "Erea Id Is Null!!!";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception) { }
            var userDto = new UserDto();
            return View(userDto);
        }

        public async Task<IActionResult> EditUser(long id)
        {
            try
            {
                if (id > 0)
                {
                    var user = await (await _userService.GetUserAsync(1, id)).AsAsyncEnumerable().ToListAsync();
                    if (user == null) return RedirectToAction(nameof(Index));
                    var users = user.FirstOrDefault();
                    var userDto = new UserDto
                    {
                        Name = users!.Name,
                        Id = users.Id,
                        userStatus = users.UserStatus,
                        CreatedAt = users.CreatedAt,
                    };
                    return View(userDto);
                }
            }
            catch (Exception) { }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(UserDto userDto)
        {
            try
            {
                var edit = await _userService.UpdateAsync(1, userDto.Id, p =>
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
            catch (Exception) { }
            return View(userDto);
        }
    }
}