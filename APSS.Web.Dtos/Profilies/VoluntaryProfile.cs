using APSS.Domain.Entities;
using AutoMapper;

namespace APSS.Web.Dtos.Profilies;

public class VoluntaryProfile : Profile
{
    public VoluntaryProfile()
    {
        CreateMap<Voluntary, VoluntaryDto>()
            .ReverseMap();
    }
}