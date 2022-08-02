namespace APSS.Web.Dtos;

public class SeasonDto : BaseAuditbleDto
{
    /// <summary>
    /// Gets or sets the name of the season
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Gets or sets the starting date of the season
    /// </summary>
    public DateTime StartsAt { get; set; }

    /// <summary>
    /// Gets or sets the starting date of the season
    /// </summary>
    public DateTime EndsAt { get; set; }
}