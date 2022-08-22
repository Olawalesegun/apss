using APSS.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace APSS.Web.Dtos;

public class IndividualDto : BaseAuditbleDto
{
    [Display(Name="  إسم الفرد")]
    public string Name { get; set; } = null!;
    public string Job { get; set; } = null!;
    public IndividualSex Sex { get; set; }
    public SocialStatusDto SocialStatus { get; set; } = null!;
    public string PhonNumber { get; set; } = null!;
    public string NationalId { get; set; } = null!;
    public string Address { get; set; } = null!;
    public UserDto User { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }
    public FamilyDto Family { get; set; } = null!;
}