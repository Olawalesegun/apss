﻿namespace APSS.Domain.Entities;

public class SurveyEntry : AuditableEntity
{
    /// <summary>
    /// Gets or sets the user who made the entry
    /// </summary>
    public User MadeBy { get; set; } = null!;

    /// <summary>
    /// Gets or sets the external identifier of the entry, referencing 
    /// an external resource (e.g., NoSQL database entry)
    /// </summary>
    public string ExternalIdentifier { get; set; } = null!;
}