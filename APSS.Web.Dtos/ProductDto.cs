namespace APSS.Web.Dtos;

public class ProductDto : ConfirmableDto
{
    public UserDto AddedBy { get; set; } = null!;
}