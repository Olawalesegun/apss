using APSS.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace APSS.Web.Dtos;

public class IndividualAddDto : BaseAuditbleDto
{
    [Display(Name = "Name")]
    [Required(ErrorMessage = "Field Name is required")]
    public string Name { get; set; } = null!;

    [Display(Name = "Gender")]
    [Required(ErrorMessage = "Field Gender is required")]
    public IndividualSex Sex { get; set; }

    [Display(Name = "Address")]
    [Required(ErrorMessage = "Field Address is required")]
    public string Address { get; set; } = null!;

    [Display(Name = "Family")]
    [Required(ErrorMessage = "Field Family is required")]
    public FamilyDto Family { get; set; } = null!;

    [Display(Name = "User")]
    public UserDto User { get; set; } = null!;
}