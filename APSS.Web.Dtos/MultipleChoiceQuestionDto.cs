using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APSS.Web.Dtos;

public class MultipleChoiceQuestionDto
{
    public ICollection<MultipleChoiceAnswerItemDto> CandidateAnswers { get; set; } = null!;

    public bool CanMultiSelect { get; set; } = false;
}