using APSS.Domain.Entities;
using APSS.Web.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APSS.Web.Dtos
{
    public class AnimalProductDto : BaseAuditbleDto
    {
        public string Name { get; set; } = null!;

        public AnimalProductUnitDto Unit { get; set; } = null!;

        public double Quantity { get; set; }

        public TimeSpan PeriodTaken { get; set; }

        public AnimalGroupDto Producer { get; set; } = null!;
    }
}