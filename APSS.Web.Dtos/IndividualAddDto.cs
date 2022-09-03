using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using static APSS.Web.Dtos.IndividualDto;
using APSS.Web.Dtos.ValueTypes;

namespace APSS.Web.Dtos;

public sealed class IndividualAddDto : BaseAuditbleDto
{
    [Required]
    [RegularExpression(@"^[a-zA-Z''\s]{1,}$", ErrorMessage = "Characters are not allowed.")]
    [Display(Name = "Name")]
    public string Name { get; set; } = null!;

    [Required]
    [Display(Name = "Gender")]
    public SexDto Sex { get; set; }

    [Required]
    [Display(Name = "Address")]
    public string Address { get; set; } = null!;

    [Display(Name = "Family")]
    public FamilyDto Family { get; set; } = null!;

    [Display(Name = "User")]
    public UserDto User { get; set; } = null!;
}