using APSS.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APSS.Web.Dtos.Profilies
{
    internal class AnimalProductProfile : Profile
    {
        public AnimalProductProfile()
        {
            CreateMap<AnimalProduct, AnimalProductDto>()
                .ForMember(x => x.Producer, b => b.MapFrom(s => CreateMap<AnimalGroup, AnimalProductDto>()))
                .ForMember(x => x.Unit, b => b.MapFrom(s => CreateMap<AnimalProductUnit, AnimalProductUnitDto>()))
                .ReverseMap();
        }
    }
}