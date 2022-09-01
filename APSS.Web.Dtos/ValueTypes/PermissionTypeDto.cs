using APSS.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace APSS.Web.Dtos.ValueTypes;

public sealed class PermissionTypeDto
{
    #region Fields

    private PermissionType _permissions;

    #endregion Fields

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="permissions">The initial permission set</param>
    public PermissionTypeDto(PermissionType permissions = 0)
        => _permissions = permissions;

    #region Properties

    /// <summary>
    /// Gets or sets whether permissions has `Create` flag set or not
    /// </summary>
    [Display(Name = "Create")]
    public bool Create
    {
        get => HasPermission(PermissionType.Create);
        set => SetPermissionIf(PermissionType.Create, value);
    }

    /// <summary>
    /// Gets or sets whether permissions has `Delete` flag set or not
    /// </summary>
    [Display(Name = "Delete")]
    public bool Delete
    {
        get => HasPermission(PermissionType.Delete);
        set => SetPermissionIf(PermissionType.Delete, value);
    }

    /// <summary>
    /// Gets or sets whether permissions has `Read` flag set or not
    /// </summary>
    [Display(Name = "Read")]
    public bool Read
    {
        get => HasPermission(PermissionType.Read);
        set => SetPermissionIf(PermissionType.Read, value);
    }

    /// <summary>
    /// Gets or sets whether permissions has `Update` flag set or not
    /// </summary>
    [Display(Name = "Update")]
    public bool Update
    {
        get => HasPermission(PermissionType.Update);
        set => SetPermissionIf(PermissionType.Update, value);
    }

    /// <summary>
    /// Gets the current perission set
    /// </summary>
    public PermissionType Permissions => _permissions;

    #endregion Properties

    #region Private Methods

    private bool HasPermission(PermissionType permission)
        => _permissions.HasFlag(permission);

    private void SetPermissionIf(PermissionType permission, bool cond)
    {
        if (cond)
            _permissions |= permission;
    }

    #endregion Private Methods
}