using System.ComponentModel.DataAnnotations;

namespace APSS.Web.Dtos;

public class OwnableDto : ConfirmableDto
{
    [Display(Name = "الاسم")]
    public string Name { get; set; } = null!;

    [Display(Name = "المالك")]
    public UserDto OwnedBy { get; set; } = null!;
}