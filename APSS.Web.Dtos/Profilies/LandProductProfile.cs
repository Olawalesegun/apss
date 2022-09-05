using APSS.Domain.Entities;

using AutoMapper;

namespace APSS.Web.Dtos.Profilies;

public class LandProductProfile : Profile
{
    public LandProductProfile()
    {
        CreateMap<LandProduct, LandProductDto>().ReverseMap();
    }
}