using APSS.Domain.Entities;

using AutoMapper;

namespace APSS.Web.Dtos.Profilies;

public class LandProfile : Profile
{
    public LandProfile()
    {
        CreateMap<Land, LandDto>();
        CreateMap<LandDto, Land>();
    }
}