using APSS.Domain.Entities;
using AutoMapper;

namespace APSS.Web.Dtos.Profilies
{
    public class IndividualProfile : Profile
    {
        public IndividualProfile()
        {
            CreateMap<Individual, IndividualDto>()
                .ForMember(f => f.Family, op => op.Ignore())
                .ForMember(i => i.AddBy, op => op.MapFrom(i => i.AddedBy))
                .ReverseMap();
        }
    }
}