using APSS.Web.Dtos.ValueTypes;
using System.ComponentModel.DataAnnotations;

namespace APSS.Web.Dtos.Forms;

public class IndividualAddForm
{
    [Required]
    [Display(Name = "Name")]
    public string Name { get; set; } = null!;

    [Display(Name = "Address")]
    [Required]
    public string Address { get; set; } = null!;

    [Display(Name = "Sex")]
    [Required]
    public SexDto Sex { get; set; }

    [Required]
    public long FamilyId { get; set; }
}