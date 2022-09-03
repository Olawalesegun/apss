namespace APSS.Web.Dtos;

public class FamilyGetDto : BaseAuditbleDto
{
    public string Name { get; set; } = null!;
    public string LivingLocation { get; set; } = null!;
    public string UserName { get; set; } = null!;
}