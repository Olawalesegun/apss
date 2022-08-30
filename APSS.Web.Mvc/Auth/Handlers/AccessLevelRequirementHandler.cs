using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using APSS.Web.Mvc.Auth.Requirements;

namespace APSS.Web.Mvc.Auth.Handlers;

public sealed class AccessLevelRequirementHandler : AuthorizationHandler<AccessLevelRequirement>
{
    /// <inheritdoc/>
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        AccessLevelRequirement requirement)
    {
        if (context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role) is var role &&
            role?.Value == requirement.AccessLevel.ToString())
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}