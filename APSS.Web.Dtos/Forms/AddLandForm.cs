using System.ComponentModel.DataAnnotations;

namespace APSS.Web.Dtos.Forms;

public class AddLandForm
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; } = null!;

    [Display(Name = "Area")]
    [Required(ErrorMessage = "Area is required")]
    public long Area { get; set; }

    [Display(Name = "Longitude")]
    [Range(-180, 180)]
    [Required(ErrorMessage = "Longitude is required")]
    public double Longitude { get; set; }

    [Display(Name = "Latitude")]
    [Range(-90, 90)]
    [Required(ErrorMessage = "Latitude is required")]
    public double Latitude { get; set; }

    [Required(ErrorMessage = "Address is required")]
    [Display(Name = "Address")]
    public string Address { get; set; } = null!;

    [Display(Name = "Is Usable")]
    public bool IsUsable { get; set; }

    [Display(Name = "Is Used")]
    public bool IsUsed { get; set; } = false;
}