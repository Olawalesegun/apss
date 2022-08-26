using APSS.Web.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APSS.Web.Dtos
{
    public class AnimalProductUnitDto : BaseAuditbleDto
    {
        [Display(Name = "اسم الوحدة")]
        public string Name { get; set; } = null!;

        public static implicit operator uint(AnimalProductUnitDto v)
        {
            throw new NotImplementedException();
        }
    }
}