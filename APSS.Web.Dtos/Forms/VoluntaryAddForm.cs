using System.ComponentModel.DataAnnotations;

namespace APSS.Web.Dtos.Forms;

public class VoluntaryAddForm
{
    [Display(Name = "Name")]
    [Required]
    public string Name { get; set; } = null!;

    [Display(Name = "Field ")]
    [Required]
    public string Field { get; set; } = null!;

    [Required]
    public long IndividualId { get; set; }
}