using Microsoft.AspNetCore.Authorization;
using APSS.Domain.Entities;
using APSS.Web.Mvc.Auth.Requirements;

namespace APSS.Web.Mvc.Auth.Handlers;

public sealed class PermissionRequirementHandler : AuthorizationHandler<PermissionRequirement>
{
    /// <inheritdoc/>
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        if (context.User.Claims.
                    FirstOrDefault(c => c.Type == CustomClaims.Permissions) is var permissionsStr
            && permissionsStr is null)
        {
            return Task.CompletedTask;
        }

        var claimedPermissions = permissionsStr.Value.Split(',');
        var requiredPermissions = requirement.Permission.GetPermissionValues();

        if (requiredPermissions.All(claimedPermissions.Contains))
            context.Succeed(requirement);

        return Task.CompletedTask;
    }
}