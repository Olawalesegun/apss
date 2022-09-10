using System.ComponentModel.DataAnnotations;

namespace APSS.Web.Dtos;

public class LandProductUnitDto : BaseAuditbleDto
{
    [Display(Name = "Name")]
    [Required]
    [MinLength(3, ErrorMessage = "must be more than 2 digit")]
    public string Name { get; set; } = null!;
}