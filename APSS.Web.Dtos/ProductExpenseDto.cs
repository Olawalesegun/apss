﻿using APSS.Web.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APSS.Web.Dtos
{
    public class ProductExpenseDto : BaseAuditbleDto
    {
        [Required(ErrorMessage = "يجب ادخال نوع التكلقة")]
        [Display(Name = "نوع النكلفة")]
        public string Type { get; set; } = null!;

        [Required(ErrorMessage = "يجب ادخال قيمة التكلقة")]
        [Display(Name = "التكلفة")]
        public decimal Price { get; set; }

        public int ProductId { get; set; }

        public ProductDto SpentOn { get; set; } = null!;
    }
}