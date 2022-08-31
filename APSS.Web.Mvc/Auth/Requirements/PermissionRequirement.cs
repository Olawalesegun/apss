using Microsoft.AspNetCore.Authorization;

using APSS.Domain.Entities;

namespace APSS.Web.Mvc.Auth.Requirements;

public sealed class PermissionRequirement : IAuthorizationRequirement
{
    private readonly PermissionType _permissions;

    public PermissionRequirement(PermissionType permission)
        => _permissions = permission;

    public PermissionType Permission => _permissions;
}