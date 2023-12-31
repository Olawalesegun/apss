﻿namespace APSS.Domain.Entities;

/// <summary>
/// A class to represent the voluntary
/// </summary>
public sealed class Voluntary : AuditableEntity
{
    /// <summary>
    /// Gets or sets the name of the voluntary
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Gets or sets the field of the voluntary
    /// </summary>
    public string Field { get; set; } = null!;

    /// <summary>
    /// Gets or sets the idnividual who offered to volunteer
    /// </summary>
    public Individual OfferedBy { get; set; } = null!;
}