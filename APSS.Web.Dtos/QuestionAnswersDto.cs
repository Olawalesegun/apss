using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APSS.Web.Dtos
{
    public class QuestionAnswersDto : BaseAuditbleDto
    {
        [Display(Name = "Question")]
        public QuestionDto Question { get; set; } = new QuestionDto();
    }
}