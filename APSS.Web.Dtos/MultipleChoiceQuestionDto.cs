using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APSS.Web.Dtos;

public class MultipleChoiceQuestionDto
{
    [Display(Name = "Choices")]
    public ICollection<MultipleChoiceAnswerItemDto> CandidateAnswers { get; set; } = null!;

    [Display(Name = "Multiple Choice?")]
    public bool CanMultiSelect { get; set; } = false;
}