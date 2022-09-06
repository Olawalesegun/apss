﻿using APSS.Domain.Entities;
using APSS.Web.Dtos;
using APSS.Web.Dtos.ValueTypes;
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
        [Display(Name = "Type")]
        [Required(ErrorMessage = "Type is Required")]
        public string Type { get; set; } = null!;

        [Required(ErrorMessage = "Quantity is Required")]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [Display(Name = "Sex")]
        public SexDto Sex { get; set; }

        [Display(Name = "Name")]
        public string? Name { get; set; }
        public bool? IsConfirmed { get; set; }
    }
}