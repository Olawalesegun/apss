using System.ComponentModel.DataAnnotations;

namespace APSS.Web.Dtos
{
    public class FamilyIndividualDto : BaseAuditbleDto
    {
        [Display(Name = "Family")]
        [Required(ErrorMessage = "Field Family is required")]
        public FamilyDto Family { get; set; } = null!;

        [Display(Name = "Individual")]
        [Required(ErrorMessage = "Field Individual is required")]
        public IndividualDto Individual { get; set; } = null!;

        [Display(Name = "Proveder")]
        public bool IsProvider { get; set; } = false;

        [Display(Name = "Parenter")]
        public bool IsParent { get; set; }
    }
}