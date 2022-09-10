using APSS.Domain.Entities;
using AutoMapper;

namespace APSS.Web.Dtos.Profilies;

public class ExpensesProfile : Profile
{
    public ExpensesProfile()
    {
        CreateMap<ProductExpense, ProductExpenseDto>()
            .ReverseMap();
    }
}