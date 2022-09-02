using System.ComponentModel.DataAnnotations;

namespace APSS.Web.Dtos.ValueTypes;

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