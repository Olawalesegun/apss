using APSS.Domain.Entities;
using AutoMapper;

namespace APSS.Web.Dtos.Profilies;

public class SurveyProfile : Profile
{
    public SurveyProfile()
    {
        CreateMap<Survey, SurveyDto>()
            .ForMember(s => s.CreatedBy, s => s.MapFrom(s => s.CreatedBy))
            .ForMember(s => s.Entries, s => s.MapFrom(s => s.Entries))
            .ForMember(s => s.Questions, s => s.MapFrom(s => s.Questions))
            .ReverseMap();
    }
}