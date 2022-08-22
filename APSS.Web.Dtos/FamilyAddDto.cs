using System.ComponentModel.DataAnnotations;

namespace APSS.Web.Dtos;

public class FamilyAddDto : BaseAuditbleDto
{
    [Required(ErrorMessage = "يجب ادخال اسم العائلة")]
    [Display(Name = "إسم العائلة")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = " يجب ادخال عنوان سكن العائلة")]
    [Display(Name = "  عنوان العائلة ")]
    public string LivingLocation { get; set; } = null!;

    public UserDto User { get; set; } = null!;
}