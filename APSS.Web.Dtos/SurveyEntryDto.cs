using APSS.Domain.Entities;

namespace APSS.Web.Dtos;

public class SurveyEntryDto : BaseAuditbleDto
{
    public UserDto MadeBy { get; set; } = null!;

    public SurveyDto Survey { get; set; } = null!;

    public ICollection<QuestionAnswer> Answers { get; set; } = new List<QuestionAnswer>();
}