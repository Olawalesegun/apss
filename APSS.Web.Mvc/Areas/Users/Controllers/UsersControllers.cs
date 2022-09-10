using Microsoft.AspNetCore.Mvc;
using APSS.Domain.Entities;
using APSS.Domain.Repositories;
using APSS.Domain.Services;
using APSS.Web.Dtos;
using APSS.Web.Dtos.Forms;
using APSS.Web.Dtos.Parameters;
using APSS.Web.Mvc.Auth;
using APSS.Web.Mvc.Models;
using APSS.Web.Mvc.Util.Navigation.Routes;
using AutoMapper;

namespace APSS.Web.Mvc.Areas.Users.Controllers;

[Area(Areas.Users)]
public class UsersController : Controller
{
    private readonly IUsersService _userService;
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public UsersController(IUsersService userService, IUnitOfWork uow, IMapper mapper)
    {
        _userService = userService;
        _uow = uow;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Index([FromQuery] FilteringParameters args)
    {
        var ret = await (await _userService.GetSubuserAsync(User.GetAccountId()))
            .Where(u => u.Name.Contains(args.Query))
            .Page(args.Page, args.PageLength)
            .AsAsyncEnumerable()
            .Select(_mapper.Map<UserDto>)
            .ToListAsync();

        return View(new CrudViewModel<UserDto>(ret, args));
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
                if (ModelState.IsValid)
                {
                    var add = await _userService.CreateAsync(User.GetAccountId(), user.Name);
                    TempData["Action"] = "Add Erea";
                    TempData["success"] = $"{user.Name} is addedd successfully";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception)
            {
                return LocalRedirect(Routes.Dashboard.Users.FullPath);
            }
            return View(user);
        }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var user = await (await _userService.GetUserAsync(User.GetAccountId(), id)).FirstAsync();

        public async Task<IActionResult> UserDetials(int id)
        {
            var user = await (await _userService.GetUserAsync(User.GetAccountId(), id)).AsAsyncEnumerable().ToListAsync();
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
            var user = await (await _userService.GetUserAsync(User.GetAccountId(), id)).AsAsyncEnumerable().ToListAsync();
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

        public async Task<IActionResult> ConfirmDeleteUser(long id)
        {
            var user = await (await _userService.GetUserAsync(1, id)).AsAsyncEnumerable().ToListAsync();
            if (user == null) return NotFound();
            var delete = await _userService.SetUserStatusAsync(1, id, UserStatus.Terminated);
            if (delete == null) return RedirectToAction(nameof(Index));
            TempData["Action"] = "Delete Erea";
            TempData["success"] = "Erea Deleted Successfully";
            return LocalRedirect(Routes.Dashboard.Users.FullPath);
        }

        public async Task<IActionResult> EditUser(long id)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(UserDto userDto)
        {
            var edit = await _userService.UpdateAsync(1, userDto.Id, p =>
                 {
                     p.Name = userDto.Name;
                     p.UserStatus = userDto.userStatus;
                 });
            TempData["Action"] = "Update Erea";
            TempData["success"] = "Update Failed!!!";

        return LocalRedirect(Routes.Dashboard.Users.FullPath);
    }
}