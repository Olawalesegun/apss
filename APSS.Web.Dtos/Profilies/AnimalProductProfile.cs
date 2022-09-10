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
            CreateMap<AnimalProduct, AnimalProductDetailsDto>().ReverseMap();
        }
    }
}