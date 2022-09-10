﻿using APSS.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace APSS.Web.Dtos;

public class SurveyEntryDto : BaseAuditbleDto
{
    [Display(Name = "Made By")]
    public User MadeBy { get; set; } = null!;

    [Display(Name = "Survey")]
    public Survey Survey { get; set; } = null!;

    [Display(Name = "Type Of Answer")]
    public TypeAnswerDto Answers { get; set; } = null!;
}