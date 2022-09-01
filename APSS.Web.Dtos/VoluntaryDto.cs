using System.ComponentModel.DataAnnotations;

namespace APSS.Web.Dtos;

public class VoluntaryDto : BaseAuditbleDto
{
    [Display(Name = "Name")]
    [Required(ErrorMessage = "Field Name is required")]
    public string Name { get; set; } = null!;

    [Display(Name = "Field")]
    [Required(ErrorMessage = "Field Field is required")]
    public string Field { get; set; } = null!;

    [Display(Name = "Person")]
    [Required(ErrorMessage = "Field Person is required")]
    public IndividualDto Individual { get; set; } = null!;
}