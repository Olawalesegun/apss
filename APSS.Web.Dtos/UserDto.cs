﻿using APSS.Domain.Entities;
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
    public APSS.Domain.Entities.UserStatus userStatus { get; set; }

    /// <summary>
    /// Gets or sets the supervisor
    /// </summary>
    public UserDto? SupervisedBy { get; set; }
}