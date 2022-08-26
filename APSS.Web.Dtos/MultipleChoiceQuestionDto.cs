using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APSS.Web.Dtos;

public class MultipleChoiceQuestionDto : QuestionDto
{
    /// <summary>
    /// Gets or sets the collection of possible answers to this question
    /// </summary>
    public ICollection<string> CandidateAnswers { get; set; } = null!;

    /// <summary>
    /// Gets or sets whether multiple answers can be selected at a time
    /// </summary>
    public bool CanMultiSelect { get; set; } = false;
}