using APSS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APSS.Web.Dtos
{
    public class AnimalGroupProductDto
    {
        [Display(Name = "رقم")]
        public int Id { get; set; }

        [Display(Name = "النوع")]
        [Required(ErrorMessage = "!يجب ادخال نوع المنتج")]
        public string Type { get; set; } = null!;

        [Display(Name = "الكمية")]
        [Required(ErrorMessage = "!يجب ادخال نوع المنتج")]
        [Range(0, 1000000, ErrorMessage = "اقل قيمة هي 1")]
        public int Quantity { get; set; }

        [Display(Name = "الجنس")]
        [Required(ErrorMessage = "!يجب ادخال الجنس ")]
        public AnimalSex Sex { get; set; }

        [Display(Name = "الاسم")]
        [Required(ErrorMessage = "!يجب ادخال اسم المنتج")]
        [Range(0, 1000000, ErrorMessage = "اقل قيمة هي 1")]
        public string Name { get; set; } = null!;

        [Display(Name = "تاريخ الاضافة")]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "الوحدة ")]
        [Required(ErrorMessage = "!يجب اختيار الوحدة  ")]
        public AnimalProductUnitDto Unit { get; set; } = null!;

        public double ProductQuantity { get; set; } = 0;

        public TimeSpan PeriodTaken { get; set; } = new TimeSpan();

        public AnimalGroupDto Producer { get; set; } = null!;
    }
}