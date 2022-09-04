using APSS.Domain.Entities;
using AutoMapper;

namespace APSS.Web.Dtos.Profilies;

public class LandProfile : Profile
{
    public LandProfile()
    {
        CreateMap<Land, LandDto>()
            .ForMember(d => d.OwnedBy, s => s.MapFrom(s => CreateMap<User, UserDto>()))
            .ReverseMap();
    }
}