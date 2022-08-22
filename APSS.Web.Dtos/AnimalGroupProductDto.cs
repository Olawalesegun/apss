using APSS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APSS.Web.Dtos
{
    public class AnimalGroupProductDto
    {
        public int Id { get; set; }
        public string Type { get; set; } = null!;

        public int Quantity { get; set; }

        public AnimalSex Sex { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        public AnimalProductUnitDto Unit { get; set; } = null!;

        public double ProductQuantity { get; set; } = 0;

        public TimeSpan PeriodTaken { get; set; } = new TimeSpan();

        public AnimalGroupDto Producer { get; set; } = null!;
    }
}