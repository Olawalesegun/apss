using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APSS.Web.Dtos
{
    public class QuestionWithAnswerDto
    {
        public QuestionDto Question { get; set; } = null!;
        public string Answer { get; set; } = null!;
    }
}