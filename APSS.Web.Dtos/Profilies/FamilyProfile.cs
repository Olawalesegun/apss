using APSS.Domain.Entities;
using AutoMapper;

namespace APSS.Web.Dtos.Profilies;

public class FamilyProfile : Profile
{
    public FamilyProfile()
    {
        CreateMap<Family, FamilyDto>()
            .ForMember(f => f.AddBy, f => f.MapFrom(f => f.AddedBy
            ))
            .ReverseMap();
    }
}