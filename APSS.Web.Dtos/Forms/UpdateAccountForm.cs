using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using APSS.Domain.Entities;
using APSS.Web.Dtos.ValueTypes;

namespace APSS.Web.Dtos.Forms;

public class UpdateAccountForm
{
    [Required]
    public long Id { get; set; }

    [Required]
    [DisplayName("Holder name")]
    public string HolderName { get; set; } = null!;

    [DisplayName("Phone number")]
    public string? PhoneNumber { get; set; }

    [DisplayName("National ID")]
    public string? NationalId { get; set; }

    [DisplayName("Job")]
    public string? Job { get; set; }

    [DisplayName("Social Status")]
    public SocialStatus SocialStatus { get; set; }

    [Required]
    public PermissionTypeDto Permissions { get; set; } = new PermissionTypeDto();

    [Required]
    [DisplayName("Enabled")]
    public bool IsActive { get; set; } = false;
}