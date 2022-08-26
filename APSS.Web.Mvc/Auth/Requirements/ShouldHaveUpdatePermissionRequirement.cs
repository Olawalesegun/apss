namespace APSS.Web.Mvc.Auth.Requirements;

public sealed class ShouldHaveUpdatePermissionRequirement : ShouldHavePermissionsRequirement
{
    public ShouldHaveUpdatePermissionRequirement() : base(PermissionType.Update)
    {
    }
}