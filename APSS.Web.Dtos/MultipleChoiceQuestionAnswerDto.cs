using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APSS.Web.Dtos
{
    public class MultipleChoiceQuestionAnswerDto : QuestionAnswersDto
    {
        public ICollection<MultipleChoiceAnswerItemDto> Answers { get; set; } = new List<MultipleChoiceAnswerItemDto>();
    }
}