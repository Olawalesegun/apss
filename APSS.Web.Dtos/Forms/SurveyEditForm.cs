using System.ComponentModel.DataAnnotations;

namespace APSS.Web.Dtos.Forms;

public class SurveyEditForm
{
    [Required]
    public long Id { get; set; }

    [Display(Name = "Name")]
    [Required(ErrorMessage = "Field Name is required")]
    public string Name { get; set; } = null!;

    [Display(Name = "Expiration Date")]
    [Required(ErrorMessage = "Field Expiration Date is required")]
    public DateTime ExpirationDate { get; set; }

    [Display(Name = "Active")]
    public bool IsActive { get; set; }
}