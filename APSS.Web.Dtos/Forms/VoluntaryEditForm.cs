using System.ComponentModel.DataAnnotations;

namespace APSS.Web.Dtos.Forms;

public class VoluntaryEditForm
{
    [Required]
    public long Id { get; set; }

    [Display(Name = "Name")]
    [Required]
    public string Name { get; set; } = null!;

    [Display(Name = "Field ")]
    [Required]
    public string Field { get; set; } = null!;

    [Required]
    public long IndividualId { get; set; }
}