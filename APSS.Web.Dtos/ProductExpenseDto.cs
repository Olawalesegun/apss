using APSS.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace APSS.Web.Dtos;

public class ProductExpenseDto : BaseAuditbleDto
{
    [Required(ErrorMessage = "Type Expense is Require")]
    [Display(Name = "Expense Type")]
    [MinLength(4, ErrorMessage = "must be more than 3 digit")]
    public string Type { get; set; } = null!;

    [Required(ErrorMessage = "Price is Required")]
    [Display(Name = "Price")]
    [Range(1, double.MaxValue)]
    public decimal Price { get; set; }

    public long ProductId { get; set; }

    [Display(Name = "Spent On")]
    public Product SpentOn { get; set; } = null!;
}
;