using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APSS.Web.Dtos
{
    public class SurveyEntryDetailsDto : BaseAuditbleDto
    {
        [Display(Name = "Made By")]
        public UserDto MadeBy { get; set; } = null!;

        [Display(Name = "Survey")]
        public SurveyDto Survey { get; set; } = null!;

        [Display(Name = "Answers")]
        public ICollection<QuestionWithAnswerDto> Answers { get; set; } = new List<QuestionWithAnswerDto>();
    }
}