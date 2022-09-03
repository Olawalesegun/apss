using APSS.Domain.Entities;
using AutoMapper;

namespace APSS.Web.Dtos.Profilies;

public class UserProfile : Profile
{
    public class FamilyProfile : Profile
    {
        public FamilyProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}