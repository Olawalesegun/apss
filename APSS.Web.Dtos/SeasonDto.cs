using System.ComponentModel.DataAnnotations;

namespace APSS.Web.Dtos;

public class SeasonDto : BaseAuditbleDto
{
    /// <summary>
    /// Gets or sets the name of the season
    /// </summary>
    [Display(Name = "إسم الموسم")]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Gets or sets the starting date of the season
    /// </summary>
    [Display(Name = "تاريخ البداية")]
    public DateTime StartsAt { get; set; }

    /// <summary>
    /// Gets or sets the starting date of the season
    /// </summary>
    [Display(Name = "تاريخ النهاية")]
    public DateTime EndsAt { get; set; }
}