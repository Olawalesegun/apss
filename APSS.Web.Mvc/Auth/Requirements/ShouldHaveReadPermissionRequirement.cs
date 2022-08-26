namespace APSS.Web.Mvc.Auth.Requirements;

public sealed class ShouldHaveReadPermissionRequirement : ShouldHavePermissionsRequirement
{
    public ShouldHaveReadPermissionRequirement() : base(PermissionType.Read)
    {
    }
}