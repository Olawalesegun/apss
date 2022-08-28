using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APSS.Web.Dtos
{
    public class TextQuestionAnswerDto : QuestionAnswersDto
    {
        public string? Answer { get; set; }
    }
}