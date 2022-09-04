using APSS.Domain.Entities;
using AutoMapper;

namespace APSS.Web.Dtos.Profilies;

public class LandProductProfile : Profile
{
    public LandProductProfile()
    {
        CreateMap<LandProduct, LandProductDto>()
            .ForMember(d => d.AddedBy, f => f.MapFrom(s => CreateMap<User, UserDto>()))
            .ForMember(d => d.Unit, f => f.MapFrom(f => CreateMap<LandProductUnit, LandProductUnitDto>()))
            .ForMember(d => d.ProducedIn, f => f.MapFrom(s => CreateMap<Season, SeasonDto>()))
            .ForMember(d => d.Producer, f => f.MapFrom(s => CreateMap<Land, LandDto>()))
            .ReverseMap();
    }
}