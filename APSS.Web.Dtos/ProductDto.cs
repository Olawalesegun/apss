using APSS.Web.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APSS.Web.Dtos
{
    public class ProductDto : BaseAuditbleDto
    {
        public UserDto AddedBy { get; set; } = null!;
    }
}