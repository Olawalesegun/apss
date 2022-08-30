using APSS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APSS.Web.Dtos
{
    public class AnimalGroupProductDto : BaseAuditbleDto
    {
        [Display(Name = "Type")]
        [Required(ErrorMessage = "Type is Required")]
        public string Type { get; set; } = null!;

        [Display(Name = "Quantity")]
        [Required(ErrorMessage = "Quantity is Required")]
        [Range(0, 1000000, ErrorMessage = "")]
        public int Quantity { get; set; }

        [Display(Name = "Sex")]
        public AnimalSex Sex { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; } = null!;

        [Display(Name = "Unit ")]
        [Required(ErrorMessage = "Unit is Required ")]
        public AnimalProductUnitDto Unit { get; set; } = null!;

        [Display(Name = "Period Taken ")]
        public TimeSpan PeriodTaken { get; set; } = new TimeSpan();

        public AnimalGroupDto Producer { get; set; } = null!;
    }
}