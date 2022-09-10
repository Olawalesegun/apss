using System.ComponentModel.DataAnnotations;

namespace APSS.Web.Dtos;

public abstract class BaseAuditbleDto : BaseDto
{
    [Display(Name = "Last modification")]
    public DateTime ModifiedAt { get; set; }
}