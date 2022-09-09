using APSS.Domain.Entities;
using APSS.Web.Dtos.ValueTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APSS.Web.Dtos
{
    public class AnimalGroupListDto : BaseAuditbleDto
    {
        [Display(Name = "Type")]
        [Required(ErrorMessage = "Type is Required")]
        public string Type { get; set; } = null!;

        [Required(ErrorMessage = "Quantity is Required")]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [Display(Name = "Sex")]
        public AnimalSex Sex { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; } = null!;

        [Display(Name = "Confirm")]
        public bool Confirm { get; set; }
    }
}