using System.ComponentModel.DataAnnotations;

namespace APSS.Web.Dtos.Forms;

public class AddLandForm
{
    [Required(ErrorMessage = "Name is required")]
    [MinLength(4, ErrorMessage = "must be more than 3 charectars")]
    public string Name { get; set; } = null!;

    [Display(Name = "Area")]
    [Required]
    public long Area { get; set; }

    [Display(Name = "Longitude")]
    [Range(-180, 180, ErrorMessage = "Area must be between [-180, 180]")]
    [Required]
    public double Longitude { get; set; }

    [Display(Name = "Latitude")]
    [Range(-90, 90, ErrorMessage = "Area must be between [-90, 90]")]
    [Required]
    public double Latitude { get; set; }

    [Required]
    [Display(Name = "Address")]
    [MinLength(5, ErrorMessage = "Address must be more than 4 charectars")]
    public string Address { get; set; } = null!;

    [Display(Name = "Is Usable")]
    public bool IsUsable { get; set; }

    [Display(Name = "Is Used")]
    public bool IsUsed { get; set; } = false;
}