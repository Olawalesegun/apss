using System.ComponentModel.DataAnnotations;

namespace APSS.Web.Dtos;

public class SeasonDto : BaseAuditbleDto
{
    /// <summary>
    /// Gets or sets the name of the season
    /// </summary>
    [Display(Name = "Name")]
    [Required]
    [MinLength(3, ErrorMessage = "must be more than 2 digit")]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Gets or sets the starting date of the season
    /// </summary>
    [Display(Name = "Starts At")]
    [Required]
    [DataType(DataType.Date)]
    [CustomValidation.StartDate(ErrorMessage = "back date is not allowd")]
    public DateTime StartsAt { get; set; }

    /// <summary>
    /// Gets or sets the starting date of the season
    /// </summary>
    [Display(Name = "Ends At")]
    [Required]
    [DataType(DataType.Date)]
    [CustomValidation.StartDate(ErrorMessage = "back date is not allowd")]
    public DateTime EndsAt { get; set; }
}