using System.ComponentModel.DataAnnotations;

namespace APSS.Web.Dtos.Forms;

public class FamilyEditForm
{
    [Required]
    public long Id { get; set; }

    [Required]
    [Display(Name = "Name")]
    public string Name { get; set; } = null!;

    [Required]
    [Display(Name = "Address")]
    public string LivingLocation { get; set; } = null!;
}