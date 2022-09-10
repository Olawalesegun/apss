using APSS.Web.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APSS.Web.Dtos
{
    public class AnimalProductUnitDto : BaseAuditbleDto
    {
        [Display(Name = "Name ")]
        [Required(ErrorMessage = "Unit Name Is Required")]
        public string Name { get; set; } = null!;
    }
}