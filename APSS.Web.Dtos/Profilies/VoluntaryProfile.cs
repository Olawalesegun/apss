using APSS.Domain.Entities;
using AutoMapper;

namespace APSS.Web.Dtos.Profilies;

public class VoluntaryProfile : Profile
{
    public VoluntaryProfile()
    {
        CreateMap<Voluntary, VoluntaryDto>()
            .ForMember(v => v.OfferedBy, v => v.MapFrom(v => v.OfferedBy))
            .ReverseMap();
    }
}