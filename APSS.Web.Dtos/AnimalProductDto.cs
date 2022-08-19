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
        [Required(ErrorMessage = "يجب ادخال اسم المنتج")]
        [Display(Name = "اسم المنتج")]
        public string Name { get; set; } = null!;

        public int UnitId { get; set; }

        [Display(Name = "الوحدة ")]
        [Required(ErrorMessage = "يجب اختيار نوع الوحدة ")]
        public IEnumerable<AnimalProductUnitDto> Unit { get; set; } = new List<AnimalProductUnitDto>();

        [Required(ErrorMessage = "يجب ادخال الكمية   ")]
        [Range(1, int.MaxValue, ErrorMessage = " اقل كمية هي 1")]
        [Display(Name = "كمية المنتج")]
        public double Quantity { get; set; }

        [Display(Name = "الفترة المأخوذة")]
        public TimeSpan PeriodTaken { get; set; }

        public AnimalGroupDto Producer { get; set; } = null!;
        public int ProducerId { get; set; }
    }
}