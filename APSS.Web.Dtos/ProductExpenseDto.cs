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
        [Required(ErrorMessage = "Type Expense is Require")]
        [Display(Name = "Type Expense ")]
        public string Type { get; set; } = null!;

        [Required(ErrorMessage = "Price is Require")]
        [Display(Name = "Price")]
        public decimal Price { get; set; }

        public long ProductId { get; set; }

        [Display(Name = "منفق على")]
        public ProductDto SpentOn { get; set; } = null!;
    }
}