using System.ComponentModel.DataAnnotations;

namespace APSS.Web.Dtos
{
    public class SurveyDto : BaseAuditbleDto
    {
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Field Name is required")]
        public string Name { get; set; } = null!;

        [Display(Name = "Expiration Date")]
        [Required(ErrorMessage = "Field Expiration Date is required")]
        public DateTime ExpirationDate { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }

        [Display(Name = "User")]
        public UserDto CreatedBy { get; set; } = null!;

        [Display(Name = "Questions")]
        public ICollection<QuestionDto> Questions { get; set; } = new List<QuestionDto>();

        [Display(Name = "Entries")]
        public ICollection<SurveyEntryDto> Entries { get; set; } = new List<SurveyEntryDto>();
    }
}