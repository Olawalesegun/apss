using APSS.Domain.Entities;
using AutoMapper;

namespace APSS.Web.Dtos.Profilies
{
    public class IndividualProfile : Profile
    {
        public IndividualProfile()
        {
            CreateMap<Individual, IndividualDto>()
                .ReverseMap();
        }
    }
}