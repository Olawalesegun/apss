﻿namespace APSS.Web.Dtos;

public class VoluntaryDto : BaseAuditbleDto
{
    public string Name { get; set; } = null!;
    public string Field { get; set; } = null!;
    public IndividualDto NameIndividual { get; set; } = null!;
}