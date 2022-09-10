using System.ComponentModel.DataAnnotations;

namespace APSS.Web.Dtos.Forms;

public class SurveyAddForm
{
    [Display(Name = "Name")]
    [Required(ErrorMessage = "Field Name is required")]
    public string Name { get; set; } = null!;

    [Display(Name = "Expiration Date")]
    [Required(ErrorMessage = "Field Expiration Date is required")]
    public DateTime ExpirationDate { get; set; }
}