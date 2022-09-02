namespace APSS.Web.Dtos;

public class FamilyIndividualGetDto : BaseAuditbleDto
{
    public string NameFamily { get; set; } = null!;
    public string NameIndividual { get; set; } = null!;
    public bool IsProvider { get; set; }
    public bool IsParent { get; set; }
}