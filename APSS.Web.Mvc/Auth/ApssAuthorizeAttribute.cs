using Microsoft.AspNetCore.Authorization;
using APSS.Domain.Entities;

namespace APSS.Web.Mvc.Auth;

public sealed class ApssAuthorizedAttribute : AuthorizeAttribute
{
    public ApssAuthorizedAttribute(PermissionType permissions)
    {
        Policy = String.Join(',', permissions.GetPermissionValues());
    }
}