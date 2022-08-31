using System.ComponentModel.DataAnnotations;

namespace APSS.Web.Dtos
{
    public class FamilyIndividualDto : BaseAuditbleDto
    {
        [Display(Name = "إسم العائلة")]
        [Required(ErrorMessage = "يجب إدخال إسم العائلة")]
        public FamilyDto Family { get; set; } = null!;

        [Display(Name = "إسم الفرد")]
        [Required(ErrorMessage = "يجب إدخال إسم الفرد")]
        public IndividualDto Individual { get; set; } = null!;

        [Display(Name = "هل مازال حي؟")]
        public bool IsProvider { get; set; } = false;

        [Display(Name = " هل هو المعيل للإسرة؟")]
        public bool IsParent { get; set; }
    }
}