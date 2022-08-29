using System.ComponentModel.DataAnnotations;

namespace APSS.Web.Dtos;

public class FamilyAddDto : BaseAuditbleDto
{
    [Required(ErrorMessage = "Faild Name is requird")]
    [Display(Name = "Name")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Field Address is required")]
    [Display(Name = "Address")]
    public string LivingLocation { get; set; } = null!;

    [Display(Name = "Employee")]
    public UserDto User { get; set; } = null!;
}