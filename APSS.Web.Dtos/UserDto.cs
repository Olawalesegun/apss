using APSS.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace APSS.Web.Dtos;

public sealed class UserDto : BaseAuditbleDto
{
    /// <summary>
    /// Gets or stes the name of the user
    /// </summary>
    [Required(ErrorMessage = "يجب ادخال اسم المنطقة")]
    [Display(Name = "اسم المستخدم")]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Gets or stes the access level of the user
    /// </summary>
    [Required(ErrorMessage = "يجب اختبار مستوى الوصول ")]
    [Display(Name = "مستوى الوصول")]
    public AccessLevel AccessLevel { get; set; }

    /// <summary>
    /// Gets or sets the user status
    /// </summary>

    [Display(Name = " نشط")]
    public string? Active { get; set; }

    [Display(Name = " غير نشط")]
    public string? Inactive { get; set; }

    [Display(Name = "موقف")]
    public string? Terminated { get; set; }

    [Display(Name = " حالة المنطقة")]
    public APSS.Domain.Entities.UserStatus userStatus { get; set; }

    /// <summary>
    /// Gets or sets the supervisor
    /// </summary>
    public UserDto? SupervisedBy { get; set; }
}