using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APSS.Web.Dtos
{
    public class QuestionWithAnswerDto
    {
        [Display(Name = "Question")]
        public QuestionDto Question { get; set; } = null!;

        [Display(Name = "Answer")]
        public string Answer { get; set; } = null!;
    }
}