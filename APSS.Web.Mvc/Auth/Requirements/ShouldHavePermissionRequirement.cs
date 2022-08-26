using Microsoft.AspNetCore.Authorization;

using APSS.Domain.Entities;

namespace APSS.Web.Mvc.Auth.Requirements;

public abstract class ShouldHavePermissionsRequirement : IAuthorizationRequirement
{
    private readonly PermissionType _permissions;

    public ShouldHavePermissionsRequirement(PermissionType permission)
        => _permissions = permission;

    public PermissionType Permission => _permissions;
}