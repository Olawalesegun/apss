using APSS.Web.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APSS.Web.Dtos
{
    public class ConfirmableDto : BaseAuditbleDto
    {
        public bool? IsConfirmed { get; set; }
    }
}