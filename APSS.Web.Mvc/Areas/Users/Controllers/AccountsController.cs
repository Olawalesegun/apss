using Microsoft.AspNetCore.Mvc;
using APSS.Domain.Entities;

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
public class AccountsController : Controller
{
    private readonly IAccountsService _accountsService;
    private readonly IMapper _mapper;

    public AccountsController(IAccountsService accountsService, IMapper mapper)
    {
        _accountsService = accountsService;
        _mapper = mapper;
    }

    [HttpGet]
    [ApssAuthorized(AccessLevel.All, PermissionType.Read)]
    public async Task<IActionResult> Index(long? id, [FromQuery] FilteringParameters args)
    {
        var ret = await (await _accountsService
            .GetAccountsAsync(User.GetAccountId(), id ?? User.GetUserId()))
            .Where(u => u.HolderName.Contains(args.Query ?? string.Empty))
            .Page(args.Page, args.PageLength)
            .AsAsyncEnumerable()
            .Select(_mapper.Map<AccountDto>)
            .ToListAsync();

        return View(new CrudViewModel<AccountDto>(ret, args));
    }

    [HttpGet]
    [ApssAuthorized(AccessLevel.All, PermissionType.Read)]
    public async Task<IActionResult> Details(long? id)
    {
        var account = await _accountsService.GetAccountAsync(User.GetAccountId(), id ?? User.GetAccountId());

        return View(_mapper.Map<AccountDto>(account));
    }

    [HttpGet]
    [ApssAuthorized(AccessLevel.All ^ AccessLevel.Farmer, PermissionType.Create)]
    public IActionResult Add(long id)
        => View(new AddAccountForm { UserId = id });

    [HttpPost]
    [ValidateAntiForgeryToken]
    [ApssAuthorized(AccessLevel.All ^ AccessLevel.Farmer, PermissionType.Create)]
    public async Task<IActionResult> Add([FromForm] AddAccountForm form)
    {
        var _ = await _accountsService.CreateAsync(
            User.GetAccountId(),
            form.UserId,
            form.HolderName,
            form.Password,
            form.Permissions.Permissions);

        return LocalRedirect(Routes.Dashboard.Users.Accounts.FullPath + $"?id={form.UserId}");
    }

    [HttpGet]
    [ApssAuthorized(AccessLevel.All, PermissionType.Update)]
    public async Task<IActionResult> Update(long id)
    {
        var account = await _accountsService.GetAccountAsync(User.GetAccountId(), id);

        return View(new UpdateAccountForm
        {
            Id = account.Id,
            HolderName = account.HolderName,
            NationalId = account.NationalId,
            PhoneNumber = account.PhoneNumber,
            Job = account.Job,
            SocialStatus = account.SocialStatus,
            Permissions = new(account.Permissions),
            IsActive = account.IsActive,
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [ApssAuthorized(AccessLevel.All, PermissionType.Update)]
    public async Task<IActionResult> Update([FromForm] UpdateAccountForm form)
    {
        var account = await _accountsService.UpdateAsync(User.GetAccountId(), form.Id, a =>
        {
            a.HolderName = form.HolderName;
            a.NationalId = form.NationalId;
            a.PhoneNumber = form.PhoneNumber;
            a.Job = form.Job;
            a.SocialStatus = form.SocialStatus;
            a.Permissions = form.Permissions.Permissions;
            a.IsActive = form.IsActive;
        });

        return LocalRedirect($"{Routes.Dashboard.Users.Accounts}?id={account.User.Id}");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [ApssAuthorized(AccessLevel.All, PermissionType.Update)]
    public async Task<IActionResult> UpdatePassword([FromForm] UpdatePassowrdForm form)
    {
        var account = await _accountsService.UpdatePasswordAsync(User.GetAccountId(), form.Id, form.Password);

        return LocalRedirect($"{Routes.Dashboard.Users.Accounts}?id={account.User.Id}");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [ApssAuthorized(AccessLevel.All ^ AccessLevel.Farmer, PermissionType.Create)]
    public async Task<IActionResult> Delete(long id)
    {
        await _accountsService.RemoveAsync(User.GetAccountId(), id);

        return LocalRedirect(Routes.Dashboard.Users.Accounts.FullPath);
    }
}