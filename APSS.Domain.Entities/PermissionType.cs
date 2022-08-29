namespace APSS.Domain.Entities;

/// <summary>
/// An enum to represent the possible permissions to inherit
/// </summary>
[Flags]
public enum PermissionType
{
    None = 0,
    Read = 1,
    Delete = 2,
    Update = 4,
    Create = 8,
    Full = Read | Delete | Update | Create,
}