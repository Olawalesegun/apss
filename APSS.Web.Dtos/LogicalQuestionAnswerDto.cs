using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APSS.Web.Dtos
{
    public class LogicalQuestionAnswerDto : QuestionAnswersDto
    {
        public bool? Answer { get; set; }
    }
}