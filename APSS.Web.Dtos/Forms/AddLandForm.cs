using System.ComponentModel.DataAnnotations;

namespace APSS.Web.Dtos.Forms;

public class AddLandForm
{
    [Required(ErrorMessage = "Name is required")]
    [MinLength(4, ErrorMessage = "Name must be more than 4 charectars")]
    public string Name { get; set; } = null!;

    [Display(Name = "Area")]
    [Required(ErrorMessage = "Area is required")]
    public long Area { get; set; }

    [Display(Name = "Longitude")]
    [Range(-180, 180, ErrorMessage = "Area must be between [-180, 180]")]
    [Required(ErrorMessage = "Longitude is required")]
    public double Longitude { get; set; }

    [Display(Name = "Latitude")]
    [Range(-90, 90, ErrorMessage = "Area must be between [-90, 90]")]
    [Required(ErrorMessage = "Latitude is required")]
    public double Latitude { get; set; }

    [Required(ErrorMessage = "Address is required")]
    [Display(Name = "Address")]
    [MinLength(5, ErrorMessage = "Address must be more than 4 charectars")]
    public string Address { get; set; } = null!;

    [Display(Name = "Is Usable")]
    public bool IsUsable { get; set; }

    [Display(Name = "Is Used")]
    public bool IsUsed { get; set; } = false;
}