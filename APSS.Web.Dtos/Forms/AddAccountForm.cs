using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using APSS.Web.Dtos.ValueTypes;

namespace APSS.Web.Dtos.Forms;

public sealed class AddAccountForm
{
    [Required]
    [DisplayName("Holder name")]
    public string HolderName { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;

    [Required]
    public PermissionTypeDto Permissions { get; set; } = new PermissionTypeDto();

    [Required]
    [DisplayName("Enabled")]
    public bool IsActive { get; set; } = false;

    [Required]
    public long UserId { get; set; }
}