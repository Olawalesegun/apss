﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using APSS.Domain.Entities;

namespace APSS.Web.Dtos;

public sealed class UserDto : BaseAuditbleDto
{
    /// <summary>
    /// Gets or stes the name of the user
    /// </summary>
    [Required]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Gets or stes the access level of the user
    /// </summary>
    [DisplayName("Access level")]
    public AccessLevel AccessLevel { get; set; }

    /// <summary>
    /// Gets or sets the user status
    /// </summary>
    [DisplayName("Status")]
    public UserStatus UserStatus { get; set; }

    /// <summary>
    /// Gets or sets the supervisor
    /// </summary>
    public UserDto? SupervisedBy { get; set; }
}