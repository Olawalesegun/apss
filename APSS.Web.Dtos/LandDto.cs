using System.ComponentModel.DataAnnotations;

namespace APSS.Web.Dtos;

public class LandDto : OwnableDto
{
    [Display(Name = "Area")]
    [Required]
    public long Area { get; set; }

    [Display(Name = "Longitude")]
    [Required(ErrorMessage = "Longitude is required")]
    [Range(-180, 180, ErrorMessage = "Area must be between [-180, 180]")]
    public double Longitude { get; set; }

    [Display(Name = "Latitude")]
    [Range(-90, 90, ErrorMessage = "Area must be between [-90, 90]")]
    public double Latitude { get; set; }

    [Display(Name = "Address")]
    [Required]
    [MinLength(2, ErrorMessage = "must be more than 1 digit")]
    public string Address { get; set; } = null!;

    [Display(Name = "Is Usable")]
    public bool IsUsable { get; set; }

    [Display(Name = "Is Used")]
    public bool IsUsed { get; set; } = false;

    public ICollection<LandProductDto> Products { get; set; } = new List<LandProductDto>();
}