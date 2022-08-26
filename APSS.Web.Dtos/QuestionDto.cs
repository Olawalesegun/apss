namespace APSS.Web.Dtos
{
    public class QuestionDto
    {
        public uint Index { get; set; }

        public string Text { get; set; } = null!;

        public bool IsRequired { get; set; } = false;

        public SurveyDto Survey { get; set; } = null!;

        public QuestionTypeDto QuestionType { get; set; }
    }
}