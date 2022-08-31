using APSS.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace APSS.Web.Dtos;

public class IndividualDto : BaseAuditbleDto
{
    [Display(Name = "  الإسم")]
    [Required(ErrorMessage = "يجب إدخال إسم الفرد")]
    public string Name { get; set; } = null!;

    [Display(Name = "  العمل")]
    public string Job { get; set; } = null!;

    [Display(Name = "  الجنس")]
    [Required(ErrorMessage = "يجب إدخال جنس الفرد")]
    public SexDto Sex { get; set; }

    [Display(Name = "  الحالة الإجتماعية")]
    public SocialStatusDto? SocialStatus { get; set; }

    [Display(Name = "  رقم التلفون")]
    public string? PhonNumber { get; set; }

    [Display(Name = " رقم الهوية")]
    public string? NationalId { get; set; }

    [Display(Name = " العنوان")]
    [Required(ErrorMessage = "يجب إدخال عنوان الفرد")]
    public string Address { get; set; } = null!;

    [Display(Name = "  تاريخ الميلاد")]
    public DateTime? DateOfBirth { get; set; }

    [Display(Name = "  عائلة الفرد")]
    [Required(ErrorMessage = "يجب إدخال  العائلة ")]
    public FamilyDto Family { get; set; } = null!;

    public UserDto? User { get; set; }

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