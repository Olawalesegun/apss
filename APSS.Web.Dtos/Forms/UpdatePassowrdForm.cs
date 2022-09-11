using System.ComponentModel.DataAnnotations;

namespace APSS.Web.Dtos.Forms;

public sealed class UpdatePassowrdForm
{
    [Required]
    public long Id { get; set; }

    [Required]
    public string Password { get; set; } = null!;
}