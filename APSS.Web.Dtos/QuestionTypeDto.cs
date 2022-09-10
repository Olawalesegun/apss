using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APSS.Web.Dtos
{
    public enum QuestionTypeDto
    {
        [Display(Name = "Text Question")]
        TextQuestion,

        [Display(Name = "Logical Question")]
        LogicalQuestion,

        [Display(Name = "multiple Choices")]
        MultipleChoiceQuestion,
    }
}