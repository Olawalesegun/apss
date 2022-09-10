namespace APSS.Web.Dtos
{
    public class TypeAnswerDto
    {
        public TextQuestionAnswerDto? TextQuestionAnswer { get; set; }
        public LogicalQuestionAnswerDto? LogicalQuestionAnswer { get; set; }
        public MultipleChoiceQuestionAnswerDto? MultipleChoiceQuestionAnswer { get; set; }
    }
}