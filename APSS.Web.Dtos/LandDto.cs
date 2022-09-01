using System.ComponentModel.DataAnnotations;

namespace APSS.Web.Dtos;

public class LandDto : OwnableDto
{
    [Required(ErrorMessage = "Area is required")]
    public long Area { get; set; }

    public double Longitude { get; set; }

    public double Latitude { get; set; }

    public string Address { get; set; } = null!;

    [Display(Name = "Is Usable")]
    public bool IsUsable { get; set; }

    [Display(Name = "Is Used")]
    public bool IsUsed { get; set; } = false;

    public ICollection<LandProductDto> Products { get; set; } = new List<LandProductDto>();
}