namespace APSS.Web.Dtos;

public class ConfirmableDto : BaseAuditbleDto
{
    public bool? IsConfirmed { get; set; }
}