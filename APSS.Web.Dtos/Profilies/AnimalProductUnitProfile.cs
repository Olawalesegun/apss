using APSS.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APSS.Web.Dtos.Profilies
{
    internal class AnimalProductUnitProfile : Profile
    {
        public AnimalProductUnitProfile()
        {
            CreateMap<AnimalProductUnit, AnimalProductUnitDto>().ReverseMap();
        }
    }
}