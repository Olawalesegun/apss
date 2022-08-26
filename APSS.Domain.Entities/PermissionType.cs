namespace APSS.Domain.Entities;

/// <summary>
/// An enum to represent the possible permissions to inherit
/// </summary>
[Flags]
public enum PermissionType
{
    Read = 1,
    Delete = 2,
    Update = 4,
    Create = 8,
    Full = Read | Delete | Update | Create,
}

public static class PermissionTypeExtensions
{
    /// <summary>
    /// Gets the permission values as strings
    /// </summary>
    /// <param name="permissions"></param>
    /// <returns></returns>
    public static IEnumerable<string> GetPermissionValues(this PermissionType permissions)
    {
        return new[]
        {
            PermissionType.Create,
            PermissionType.Read,
            PermissionType.Update,
            PermissionType.Delete
        }.Where(p => permissions.HasFlag(p)).Select(p => p.ToString());
    }

    /// <summary>
    /// Gets display string of permissions
    /// </summary>
    /// <param name="permissions"></param>
    /// <returns></returns>
    public static string ToFormattedString(this PermissionType permissions)
        => $"{{ {string.Join(", ", permissions.GetPermissionValues())} }}";
}