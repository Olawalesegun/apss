using APSS.Domain.Entities;

namespace APSS.Web.Dtos;

public class IndividualDto : BaseAuditbleDto
{
    public string Name { get; set; } = null!;
    public string Job { get; set; } = null!;
    public IndividualSex Sex { get; set; }
    public SocialStatus SocialStatus { get; set; }
    public string PhonNumber { get; set; } = null!;
    public string NationalId { get; set; } = null!;
    public string Address { get; set; } = null!;
    public UserDto? User { get; set; }
    public DateTime DateOfBirth { get; set; }
    public FamilyDto Family { get; set; } = null!;
}