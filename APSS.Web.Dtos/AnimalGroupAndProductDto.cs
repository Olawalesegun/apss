using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APSS.Web.Dtos
{
    public class AnimalGroupAndProductDto
    {
        public int AnimalId { get; set; }
        public IEnumerable<AnimalGroupDto> AnimalGroupDtos { get; set; } = new List<AnimalGroupDto>();
        public AnimalGroupDto AnimalGroup { get; set; } = new AnimalGroupDto();
        public int ProductId { get; set; }
        public AnimalProductDto? AnimalProducts { get; set; }
    }
}