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
    public class AnimalProductDto : BaseAuditbleDto
    {
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; } = null!;

        public int UnitId { get; set; }

        [Display(Name = "Unit ")]
        [Required(ErrorMessage = "Unit is Required ")]
        public IEnumerable<AnimalProductUnitDto> Unit { get; set; } = new List<AnimalProductUnitDto>();

        [Display(Name = "Quantity")]
        [Required(ErrorMessage = "Quantity is Required")]
        [Range(0, 1000000, ErrorMessage = "")]
        public double Quantity { get; set; }

        [Display(Name = "Period Taken ")]
        public TimeSpan PeriodTaken { get; set; }

        public AnimalGroupDto Producer { get; set; } = null!;
        public int ProducerId { get; set; }
    }
}