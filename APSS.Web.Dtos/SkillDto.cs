using System.ComponentModel.DataAnnotations;

namespace APSS.Web.Dtos;

public class SkillDto : BaseAuditbleDto
{
    [Display(Name = "Name")]
    [Required(ErrorMessage = "Field Name is required")]
    public string Name { get; set; } = null!;

    [Display(Name = "Description")]
    public string? Description { get; set; }

    [Display(Name = "Field ")]
    [Required(ErrorMessage = "Field Field is required")]
    public string Field { get; set; } = null!;

    [Display(Name = "Person")]
    [Required(ErrorMessage = "Field Persone is required")]
    public string IndividualName { get; set; } = null!;
}