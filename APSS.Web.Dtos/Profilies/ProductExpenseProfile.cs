using APSS.Domain.Entities;
using AutoMapper;

namespace APSS.Web.Dtos.Profilies;

public class ProductExpenseProfile : Profile
{
    public ProductExpenseProfile()
    {
        CreateMap<ProductExpense, ProductExpenseDto>()
            .ForMember(d => d.SpentOn, f => f.MapFrom(s => CreateMap<LandProduct, LandProductDto>()))
            .ReverseMap();
    }
}