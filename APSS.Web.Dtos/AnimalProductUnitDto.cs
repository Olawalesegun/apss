using APSS.Web.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APSS.Web.Dtos
{
    public class AnimalProductUnitDto : BaseAuditbleDto
    {
        public string Name { get; set; } = null!;
    }
}