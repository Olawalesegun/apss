using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APSS.Web.Dtos
{
    public enum UserStatusDto
    {
        [Display(Name = " نشط")]
        Active,

        [Display(Name = " غير نشط")]
        Inactive,

        [Display(Name = "موقف")]
        Terminated,
    }
}