using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APSS.Web.Dtos
{
    public class ConfirmationDto
    {
        public IEnumerable<AnimalGroupConfirmDto> AnimalGroups { get; set; } = new List<AnimalGroupConfirmDto>();
        public IEnumerable<AnimalProductDetailsDto> AnimalProducts { get; set; } = new List<AnimalProductDetailsDto>();
        public AnimalGroupDto AnimalGroup { get; set; } = new AnimalGroupDto();
        public AnimalProductDto Products { get; set; } = new AnimalProductDto();
    }
}