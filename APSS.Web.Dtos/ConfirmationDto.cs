using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APSS.Web.Dtos
{
    public class ConfirmationDto
    {
        public IEnumerable<AnimalGroupDto> AnimalGroups { get; set; } = new List<AnimalGroupDto>();
        public IEnumerable<AnimalProductDto> AnimalProducts { get; set; } = new List<AnimalProductDto>();
    }
}