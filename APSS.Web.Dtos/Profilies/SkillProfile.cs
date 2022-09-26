using APSS.Domain.Entities;
using AutoMapper;

namespace APSS.Web.Dtos.Profilies;

public class SkillProfile : Profile
{
    public SkillProfile()
    {
        CreateMap<Skill, SkillDto>()
            .ForMember(s => s.BelongsTo, s => s.MapFrom(s => s.BelongsTo))
            .ReverseMap();
    }
}