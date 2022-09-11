using APSS.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace APSS.Web.Dtos;

public class IndividualDto : BaseAuditbleDto
{
    [Required]
    [Display(Name = "Name")]
    public string Name { get; set; } = null!;

    [Display(Name = "Job")]
    public string Job { get; set; } = null!;

    [Display(Name = "Sex")]
    [Required(ErrorMessage = "Field Sex is required")]
    public IndividualSex Sex { get; set; }

    [Display(Name = "Social Status")]
    public SocialStatus SocialStatus { get; set; }

    [Display(Name = "Phone Number")]
    public string? PhonNumber { get; set; }

    [Display(Name = "National Number")]
    public string? NationalId { get; set; }

    [Display(Name = "Address")]
    [Required(ErrorMessage = "Field Address is required")]
    public string Address { get; set; } = null!;

    [Display(Name = "Birth Date")]
    public DateTime? DateOfBirth { get; set; }

    [Display(Name = "User")]
    public UserDto AddBy { get; set; } = null!;
}