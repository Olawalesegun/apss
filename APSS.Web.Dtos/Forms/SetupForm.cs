using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace APSS.Web.Dtos.Forms;

public sealed class SetupForm
{
    [Required]
    [DisplayName("Holder name")]
    public string HolderName { get; set; } = null!;

    [Required]
    [DisplayName("Root password")]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
    public string Password { get; set; } = null!;
}