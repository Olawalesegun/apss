using APSS.Domain.Entities;
using AutoMapper;

namespace APSS.Web.Dtos.Profilies;

public class LandProductUnitProfile : Profile
{
    public LandProductUnitProfile()
    {
        CreateMap<LandProductUnit, LandProductUnitDto>()
            .ReverseMap();
    }
}