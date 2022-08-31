namespace APSS.Web.Dtos
{
    public class QuestionDto : BaseAuditbleDto
    {
        public uint Index { get; set; }

        public string Text { get; set; } = null!;

        public bool IsRequired { get; set; } = false;

        public SurveyDto Survey { get; set; } = null!;

        public QuestionTypeDto QuestionType { get; set; }
        public ICollection<MultipleChoiceAnswerItemDto>? CandidateAnswers { get; set; }
        public bool CanMultiSelect { get; set; } = false;
    }
}