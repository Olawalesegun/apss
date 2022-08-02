namespace APSS.Web.Dtos;

public class ProductExpenseDto : BaseAuditbleDto
{
    public string Type { get; set; } = null!;

    public decimal Price { get; set; }

    public ProductDto SpentOn { get; set; } = null!;
}