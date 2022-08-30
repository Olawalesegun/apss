using APSS.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace APSS.Web.Dtos;

public class IndividualAddDto : BaseAuditbleDto
{
    [Display(Name = "  :إسم الفرد")]
    [Required(ErrorMessage = "يجب إدخال إسم الفرد")]
    public string Name { get; set; } = null!;

    [Display(Name = "  :الجنس ")]
    [Required(ErrorMessage = "يجب إدخال جنس الفرد")]
    public IndividualSex Sex { get; set; }

    [Display(Name = " :العنوان ")]
    [Required(ErrorMessage = "يجب إدخال العنوان ")]
    public string Address { get; set; } = null!;

    [Display(Name = "  :إسم العائلة")]
    public FamilyDto Family { get; set; } = null!;

    public UserDto User { get; set; } = null!;
}