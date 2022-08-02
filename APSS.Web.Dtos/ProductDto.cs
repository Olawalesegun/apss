namespace APSS.Web.Dtos;

public class ProductDto : BaseAuditbleDto
{
    public UserDto AddedBy { get; set; } = null!;
}