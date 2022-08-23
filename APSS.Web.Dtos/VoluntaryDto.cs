using System.ComponentModel.DataAnnotations;

namespace APSS.Web.Dtos;

public class VoluntaryDto : BaseAuditbleDto
{
    [Display(Name = "إسم التطوع")]
    [Required(ErrorMessage = "يجب إدخال إسم التطوع")]
    public string Name { get; set; } = null!;

    [Display(Name = "مجال التطوع")]
    [Required(ErrorMessage = "يجب إدخال مجال التطوع")]
    public string Field { get; set; } = null!;

    public IndividualDto Individual { get; set; } = null!;
}