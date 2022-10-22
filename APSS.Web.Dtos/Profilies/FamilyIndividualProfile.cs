using APSS.Domain.Entities;
using AutoMapper;

namespace APSS.Web.Dtos.Profilies;

public class FamilyIndividualProfile : Profile
{
    public FamilyIndividualProfile()
    {
        CreateMap<FamilyIndividual, FamilyIndividualDto>()
            .ForMember(f => f.Family, op => op.MapFrom(f => f.Family))
            .ForMember(f => f.Individual, op => op.MapFrom(f => f.Individual))

        .ReverseMap();
    }
}