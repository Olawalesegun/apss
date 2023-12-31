﻿namespace APSS.Domain.Entities;

/// <summary>
/// A class to represent an animal product
/// </summary>
public sealed class AnimalProduct : Product
{
    /// <summary>
    /// Gets or sets the name of the animal's product
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Gets or sets the unit of the animal's product
    /// </summary>
    public AnimalProductUnit Unit { get; set; } = null!;

    /// <summary>
    /// Gets or sets the quantity of the animal's product
    /// </summary>
    public double Quantity { get; set; }

    /// <summary>
    /// Gets or sets the period taken to produce the animal's product
    /// </summary>
    public TimeSpan PeriodTaken { get; set; }

    /// <summary>
    /// Gets or sets the animal of the animal's product
    /// </summary>
    public AnimalGroup Producer { get; set; } = null!;
}