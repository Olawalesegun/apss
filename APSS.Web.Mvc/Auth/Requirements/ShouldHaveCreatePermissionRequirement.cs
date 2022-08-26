namespace APSS.Web.Mvc.Auth.Requirements;

public sealed class ShouldHaveCreatePermissionRequirement : ShouldHavePermissionsRequirement
{
    public ShouldHaveCreatePermissionRequirement() : base(PermissionType.Create)
    {
    }
}