using APSS.Domain.Entities;
using APSS.Web.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APSS.Web.Dtos
{
    public class AnimalGroupDto : BaseAuditbleDto
    {
        [Required]
        [Display(Name = "النوع")]
        public string Type { get; set; } = null!;

        [Required]
        [Display(Name = "الكمية")]
        public int Quantity { get; set; }

        [Required]
        [Display(Name = "الجنس")]
        public AnimalSex Sex { get; set; }
    }
}