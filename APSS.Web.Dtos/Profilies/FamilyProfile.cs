using APSS.Domain.Entities;
using AutoMapper;

namespace APSS.Web.Dtos.Profilies;

public class FamilyProfile : Profile
{
    public FamilyProfile()
    {
        CreateMap<Family, FamilyDto>()
    .ForMember(dest =>
        dest.Id,
        f => f.MapFrom(src => src.Id))
    .ForMember(dest =>
        dest.Name,
        f => f.MapFrom(src => src.Name))
    .ForMember(dest =>
        dest.LivingLocation,
        f => f.MapFrom(src => src.LivingLocation))
    .ForMember(dest =>
        dest.User,
        f => f.MapFrom(src => CreateMap<User, UserDto>()))
    .ReverseMap();
    }
}