using APSS.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace APSS.Web.Dtos;

public class IndividualDto : BaseAuditbleDto
{
    [Display(Name = "  إسم الفرد")]
    public string Name { get; set; } = null!;

    public string Job { get; set; } = null!;
    public IndividualSex Sex { get; set; }
    public SocialStatusDto? SocialStatus { get; set; }
    public string? PhonNumber { get; set; }
    public string? NationalId { get; set; }
    public string Address { get; set; } = null!;
    public UserDto User { get; set; } = null!;
    public DateTime? DateOfBirth { get; set; }
    public FamilyDto Family { get; set; } = null!;

    public enum SocialStatusDto
    {
        [Display(Name = "غير محدد")]
        Unspecified,

        [Display(Name = "عازب ")]
        Unmarried,

        [Display(Name = "متزوج")]
        Married,

        [Display(Name = "مطلق")]
        Divorced,

        [Display(Name = "أرمل")]
        Widowed,
    }

    public enum SexDto
    {
        [Display(Name = "ذكر ")]
        Male,

        [Display(Name = "أنثى")]
        Female
    }
}