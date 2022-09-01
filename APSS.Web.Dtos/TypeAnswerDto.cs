using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APSS.Web.Dtos
{
    public class TypeAnswerDto
    {
        public TextQuestionAnswerDto? TextQuestionAnswer { get; set; }
        public LogicalQuestionAnswerDto? LogicalQuestionAnswer { get; set; }
        public MultipleChoiceQuestionAnswerDto? MultipleChoiceQuestionAnswer { get; set; }
    }
}