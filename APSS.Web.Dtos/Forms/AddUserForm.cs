using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace APSS.Web.Dtos.Forms;

public sealed class AddUserForm
{
    [Required]
    [DisplayName("User name")]
    public string Name { get; set; } = null!;

    [DisplayName("Enabled")]
    public bool IsActive { set; get; } = true;
}