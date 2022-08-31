using System.ComponentModel.DataAnnotations;

namespace APSS.Web.Dtos;

public class SkillDto : BaseAuditbleDto
{
    [Display(Name = "  إسم المهارة")]
    [Required(ErrorMessage = "يجب إدخال إسم المهارة")]
    public string Name { get; set; } = null!;

    [Display(Name = "  إسم الفرد")]
    public string? Description { get; set; }

    [Display(Name = "  مجال المهارة ")]
    [Required(ErrorMessage = "يجب إدخال مجال المهارة")]
    public string Field { get; set; } = null!;

    [Display(Name = "  إسم الفرد")]
    public IndividualDto Individual { get; set; } = null!;
}