﻿using System.ComponentModel.DataAnnotations;

namespace APSS.Web.Dtos;

public class OwnableDto : ConfirmableDto
{
    [Display(Name = "Name")]
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; } = null!;

    [Display(Name = "Owned By")]
    public UserDto OwnedBy { get; set; } = null!;
}