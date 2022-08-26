namespace APSS.Web.Mvc.Auth.Requirements;

public sealed class ShouldHaveDeletePermissionRequirement : ShouldHavePermissionsRequirement
{
    public ShouldHaveDeletePermissionRequirement() : base(PermissionType.Delete)
    {
    }
}