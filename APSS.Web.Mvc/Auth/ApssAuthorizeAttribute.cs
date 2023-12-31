﻿using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using APSS.Domain.Entities;

namespace APSS.Web.Mvc.Auth;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class ApssAuthorizedAttribute : AuthorizeAttribute, IAuthorizationFilter
{
    private readonly AccessLevel _accessLevel;
    private readonly PermissionType _permissions;

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="accessLevel">The required access level</param>
    /// <param name="permissions">The required permissions</param>
    public ApssAuthorizedAttribute(AccessLevel accessLevel, PermissionType permissions)
    {
        _accessLevel = accessLevel;
        _permissions = permissions;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;

        if (!AreAccessLevelsValid(user) || !ArePermissionsValid(user))
            context.Result = new UnauthorizedResult();
    }

    public bool AreAccessLevelsValid(ClaimsPrincipal user)
        => _accessLevel.AsEnumerable().Any(user.IsInLevel);

    public bool ArePermissionsValid(ClaimsPrincipal user)
    {
        try
        {
            return user.GetPermissions().HasFlag(_permissions);
        }
        catch // TODO: Log errors
        {
            return false;
        }
    }
}