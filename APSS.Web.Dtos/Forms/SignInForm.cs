using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace APSS.Web.Dtos.Forms;

public sealed class SignInForm
{
    [Required]
    [RegularExpression(@"^[0-9]+$", ErrorMessage = "ID must consist of numbers only")]
    [DisplayName("Account ID")]
    public string AccountId { get; set; } = null!;

    [Required]
    [MinLength(1, ErrorMessage = "Password too short")]
    public string Password { get; set; } = null!;

    [DisplayName("Remember Me")]
    [DefaultValue(false)]
    public bool IsPersistent { get; set; }
}