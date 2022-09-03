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
        [Display(Name = "multiple Choices")]
        MultipleChoiceQuestion,

        [Display(Name = "Logical Question")]
        LogicalQuestion,

        [Display(Name = "TExt Question")]
        TextQuestion
    }
}