﻿namespace APSS.Domain.Entities;

/// <summary>
/// A class to represent a log
/// </summary>
public sealed class Log : AuditableEntity
{
    /// <summary>
    /// Gets or sets the message of the log
    /// </summary>
    public string Message { get; set; } = null!;

    /// <summary>
    /// Gets or sets the log severity level
    /// </summary>
    public LogSeverity Severity { get; set; }

    /// <summary>
    /// Gets or sets the timestamp of the log
    /// </summary>
    public DateTime TimeStamp { get; set; }

    /// <summary>
    /// Gets or sets the tags set of the log
    /// </summary>
    public ICollection<LogTag> Tags { get; set; } = new List<LogTag>();
}

/// <summary>
/// An enum to represent the log severity levels
/// </summary>
public enum LogSeverity
{
    Debug,
    Information,
    Warning,
    Error,
    Fatal,
}