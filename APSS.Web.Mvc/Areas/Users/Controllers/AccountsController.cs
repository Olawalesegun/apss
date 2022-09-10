﻿using Microsoft.AspNetCore.Mvc;
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
            .Where(u => u.HolderName.Contains(args.Query))
            .Page(args.Page, args.PageLength)
            .AsAsyncEnumerable()
            .Select(_mapper.Map<AccountDto>)
            .ToListAsync();

        return View(new CrudViewModel<AccountDto>(ret, args));
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

    [HttpPost]
    [ValidateAntiForgeryToken]
    [ApssAuthorized(AccessLevel.All ^ AccessLevel.Farmer, PermissionType.Create)]
    public async Task<IActionResult> Delete(long id)
    {
        await _accountsService.RemoveAsync(User.GetAccountId(), id);

        return LocalRedirect(Routes.Dashboard.Users.Accounts.FullPath);
    }
}