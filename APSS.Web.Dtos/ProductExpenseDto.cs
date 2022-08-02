using APSS.Web.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APSS.Web.Dtos
{
    public class ProductExpenseDto : BaseAuditbleDto
    {
        public string Type { get; set; } = null!;

        public decimal Price { get; set; }

        public ProductDto SpentOn { get; set; } = null!;
    }
}