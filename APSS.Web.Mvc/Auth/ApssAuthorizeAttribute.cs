using Microsoft.AspNetCore.Authorization;
using APSS.Domain.Entities;

namespace APSS.Web.Mvc.Auth;

public sealed class ApssAuthorizedAttribute : AuthorizeAttribute
{
    public ApssAuthorizedAttribute(AccessLevel accessLevel, PermissionType permissions)
    {
        var policies = permissions
            .GetPermissionValues()
            .Append(accessLevel.ToString());

        Policy = string.Join(',', policies);
    }
}