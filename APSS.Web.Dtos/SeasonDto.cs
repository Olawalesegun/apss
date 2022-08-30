using System.ComponentModel.DataAnnotations;

namespace APSS.Web.Dtos;

public class SeasonDto : BaseAuditbleDto
{
    /// <summary>
    /// Gets or sets the name of the season
    /// </summary>
    [Display(Name = "Name")]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Gets or sets the starting date of the season
    /// </summary>
    [Display(Name = "Starts At")]
    public DateTime StartsAt { get; set; }

    /// <summary>
    /// Gets or sets the starting date of the season
    /// </summary>
    [Display(Name = "Ends At")]
    public DateTime EndsAt { get; set; }
}