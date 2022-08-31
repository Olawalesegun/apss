using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APSS.Web.Dtos
{
    public class MultipleChoiceAnswerItemDto : BaseAuditbleDto
    {
        public string Value { get; set; } = null!;
    }
}