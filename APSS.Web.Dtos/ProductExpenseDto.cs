using APSS.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace APSS.Web.Dtos;

public class ProductExpenseDto : BaseAuditbleDto
{
    [Required(ErrorMessage = "Type Expense is Require")]
    [Display(Name = "Type Expense ")]
    public string Type { get; set; } = null!;

    [Required(ErrorMessage = "Ptice is Require")]
    [Display(Name = "Price")]
    public decimal Price { get; set; }

    public long ProductId { get; set; }

    [Display(Name = "Spent On")]
    public Product SpentOn { get; set; } = null!;
}
;