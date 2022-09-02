using APSS.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace APSS.Web.Dtos;

public sealed class UserDto : BaseAuditbleDto
{
    /// <summary>
    /// Gets or stes the name of the user
    /// </summary>
    [Required(ErrorMessage = "Erea Name is Required")]
    [Display(Name = "Erea Name")]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Gets or stes the access level of the user
    /// </summary>
    [Display(Name = "Access Level")]
    public AccessLevel AccessLevel { get; set; }

    /// <summary>
    /// Gets or sets the user status
    /// </summary>

    [Display(Name = " Active")]
    public string? Active { get; set; }

    [Display(Name = " Inactive")]
    public string? Inactive { get; set; }

    [Display(Name = "Termint")]
    public string? Terminated { get; set; }

    [Display(Name = "Area Status")]
    public UserStatus userStatus { get; set; }

    /// <summary>
    /// Gets or sets the supervisor
    /// </summary>
    public UserDto SupervisedBy { get; set; }
}