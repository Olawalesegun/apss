using APSS.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace APSS.Web.Dtos;

public class IndividualDto : BaseAuditbleDto
{
    [Display(Name = "Name")]
    [Required(ErrorMessage = "Field Name is required")]
    public string Name { get; set; } = null!;

    [Display(Name = "Job")]
    public string Job { get; set; } = null!;

    [Display(Name = "Sex")]
    [Required(ErrorMessage = "Field Sex is required")]
    public SexDto Sex { get; set; }

    [Display(Name = "Social Status")]
    public SocialStatusDto? SocialStatus { get; set; }

    [Display(Name = "Phone Number")]
    public string? PhonNumber { get; set; }

    [Display(Name = "National Number")]
    public string? NationalId { get; set; }

    [Display(Name = "Address")]
    [Required(ErrorMessage = "Field Address is required")]
    public string Address { get; set; } = null!;

    [Display(Name = "Birth Date")]
    public DateTime? DateOfBirth { get; set; }

    [Display(Name = "Family")]
    [Required(ErrorMessage = "Field Family is required ")]
    public FamilyDto Family { get; set; } = null!;

    public UserDto? User { get; set; }

    public enum SocialStatusDto
    {
        [Display(Name = "Unspecified")]
        Unspecified,

        [Display(Name = "Single")]
        Unmarried,

        [Display(Name = "Married")]
        Married,

        [Display(Name = "Divorced")]
        Divorced,

        [Display(Name = "Widowed")]
        Widowed,
    }

    public enum SexDto
    {
        [Display(Name = "Male ")]
        Male,

        [Display(Name = "Female")]
        Female
    }
}