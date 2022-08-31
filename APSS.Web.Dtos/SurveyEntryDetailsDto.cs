using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APSS.Web.Dtos
{
    public class SurveyEntryDetailsDto : BaseAuditbleDto
    {
        public UserDto MadeBy { get; set; } = null!;
        public SurveyDto Survey { get; set; } = null!;
        public ICollection<QuestionWithAnswerDto> Answers { get; set; } = new List<QuestionWithAnswerDto>();
    }
}