using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APSS.Web.Dtos
{
    public class MultipleChoiceAnswerItemDto : BaseAuditbleDto
    {
        [Display(Name = "Choice")]
        [Required(ErrorMessage = "Field Choice is required")]
        public string Value { get; set; } = null!;
    }
}