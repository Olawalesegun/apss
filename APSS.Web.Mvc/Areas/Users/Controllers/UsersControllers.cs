using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
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
    [ApssAuthorized(AccessLevel.All, PermissionType.Read)]
    public async Task<IActionResult> Index([FromQuery] FilteringParameters args)
    {
        var ret = await (await _userService.GetSubuserAsync(User.GetAccountId()))
            .Where(u => u.Name.Contains(args.Query ?? string.Empty))
            .Page(args.Page, args.PageLength)
            .AsAsyncEnumerable()
            .Select(_mapper.Map<UserDto>)
            .ToListAsync();

        return View(new CrudViewModel<UserDto>(ret, args));
    }

    [HttpGet]
    [ApssAuthorized(AccessLevel.All, PermissionType.Create)]
    public IActionResult Add()
        => View(new AddUserForm());

    [HttpPost]
    [ValidateAntiForgeryToken]
    [ApssAuthorized(AccessLevel.All, PermissionType.Create)]
    public async Task<IActionResult> Add(AddUserForm form)
    {
        if (!ModelState.IsValid)
            return View(form);

        var _ = await _userService.CreateAsync(
            User.GetAccountId(),
            form.Name,
            form.IsActive ? UserStatus.Active : UserStatus.Inactive);

        return LocalRedirect(Routes.Dashboard.Users.Users.FullPath);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [ApssAuthorized(AccessLevel.All ^ AccessLevel.Farmer, PermissionType.Delete)]
    public async Task<IActionResult> Delete(long id)
    {
        await _userService.RemoveAsync(User.GetAccountId(), id);

        return LocalRedirect(Routes.Dashboard.Users.Users.FullPath);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var user = await (await _userService.GetUserAsync(User.GetAccountId(), id)).FirstAsync();

        var userDto = new UserDto
        {
            Name = user.Name,
            Id = user.Id,
            AccessLevel = user.AccessLevel,
            UserStatus = user.UserStatus,
            CreatedAt = user.CreatedAt,
            ModifiedAt = user.ModifiedAt
        };
        return View(userDto);
    }

    public async Task<IActionResult> Update(long id)
    {
        var user = await (await _userService.GetUserAsync(User.GetAccountId(), id)).AsAsyncEnumerable().ToListAsync();
        if (user == null) return RedirectToAction(nameof(Index));
        var users = user.FirstOrDefault();
        var userDto = new UserDto
        {
            Name = users!.Name,
            Id = users.Id,
            UserStatus = users.UserStatus,
            CreatedAt = users.CreatedAt,
        };
        return View(userDto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(UserDto userDto)
    {
        var edit = await _userService.UpdateAsync(User.GetAccountId(), userDto.Id, p =>
        {
            p.Name = userDto.Name;
            p.UserStatus = userDto.UserStatus;
        });
        TempData["Action"] = "Update Erea";
        TempData["success"] = "Update Failed!!!";

        return LocalRedirect(Routes.Dashboard.Users.FullPath);
    }
}