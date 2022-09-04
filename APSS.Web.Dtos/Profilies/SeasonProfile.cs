using APSS.Domain.Entities;
using AutoMapper;

namespace APSS.Web.Dtos.Profilies;

public class SeasonProfile : Profile
{
    public SeasonProfile()
    {
        CreateMap<Season, SeasonDto>()
            .ReverseMap();
    }
}