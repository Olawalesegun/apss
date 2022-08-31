namespace APSS.Web.Dtos
{
    public class SurveyDto : BaseAuditbleDto
    {
        public string Name { get; set; } = null!;

        public DateTime ExpirationDate { get; set; }

        public bool IsActive { get; set; }

        public UserDto CreatedBy { get; set; } = null!;

        public ICollection<QuestionDto> Questions { get; set; } = new List<QuestionDto>();

        public ICollection<SurveyEntryDto> Entries { get; set; } = new List<SurveyEntryDto>();
    }
}