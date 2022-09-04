using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APSS.Web.Dtos
{
    public class LogicalQuestionAnswerDto : QuestionAnswersDto
    {
        [Display(Name = "Answer")]
        [Required(ErrorMessage = "Field Answer is required")]
        public bool? Answer { get; set; }
    }
}