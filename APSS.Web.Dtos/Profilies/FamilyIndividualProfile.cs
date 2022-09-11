using APSS.Domain.Entities;
using AutoMapper;

namespace APSS.Web.Dtos.Profilies;

public class FamilyIndividualProfile : Profile
{
    public FamilyIndividualProfile()
    {
        CreateMap<FamilyIndividual, FamilyIndividualDto>()

        .ReverseMap();
    }
}