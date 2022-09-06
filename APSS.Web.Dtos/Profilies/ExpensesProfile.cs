using APSS.Domain.Entities;
using AutoMapper;

namespace APSS.Web.Dtos.Profilies;

public class ExpensesProfile : Profile
{
    public ExpensesProfile()
    {
        CreateMap<ProductExpense, ProductExpenseDto>()
            .ForMember(d => d.SpentOn, f => f.MapFrom(s => CreateMap<LandProductDto, LandProduct>()))
            .ReverseMap();
    }
}