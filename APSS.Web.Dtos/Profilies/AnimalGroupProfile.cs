using APSS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace APSS.Web.Dtos.Profilies
{
    public class AnimalGroupProfile : Profile
    {
        public AnimalGroupProfile()
        {
            CreateMap<AnimalGroup, AnimalGroupDto>().ReverseMap();
        }
    }
}