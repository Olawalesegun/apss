namespace APSS.Web.Dtos;

public class OwnableDto : ConfirmableDto
{
    public string Name { get; set; } = null!;
    public UserDto OwnedBy { get; set; } = null!;
}