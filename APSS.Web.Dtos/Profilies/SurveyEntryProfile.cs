using APSS.Domain.Entities;
using AutoMapper;

namespace APSS.Web.Dtos.Profilies;

public sealed class SurveyEntryProfile : Profile
{
    public SurveyEntryProfile()
    {
        CreateMap<SurveyEntryDto, SurveyEntry>()
            .ForMember(e => e.Survey, e => e.MapFrom(e => e.Survey))
            .ForMember(e => e.MadeBy, e => e.MapFrom(e => e.MadeBy))
            .ReverseMap();
    }
}