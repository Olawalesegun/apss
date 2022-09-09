using APSS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APSS.Web.Dtos
{
    public class AnimalProductEditDto : Ownable
    {
        [Display(Name = "Quantity")]
        [Required(ErrorMessage = "Quantity is Required")]
        [Range(0, 1000000, ErrorMessage = "")]
        public double Quantity { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; } = null!;

        [Display(Name = "Unit ")]
        [Required(ErrorMessage = "Unit is Required ")]
        public AnimalProductUnit Unit { get; set; } = null!;

        [Display(Name = "Period Taken ")]
        public TimeSpan PeriodTaken { get; set; } = new TimeSpan();

        public AnimalGroup Producer { get; set; } = null!;
        public long producerId { get; set; }
        public long UnitId { get; set; }
    }
}