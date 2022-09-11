using System.ComponentModel.DataAnnotations;

namespace APSS.Web.Dtos.Parameters;

public sealed class FilteringParameters
{
    private const int _maxPageLength = 48;
    private const int _defaultPageLength = 16;

    /// <summary>
    /// Gets or sets the page index
    /// </summary>
    [Range(1, int.MaxValue)]
    public int Page { get; set; } = 1;

    /// <summary>
    /// Gets or sets the maximum page length
    /// </summary>
    [Range(1, _maxPageLength)]
    public int PageLength { get; set; } = _defaultPageLength;

    /// <summary>
    /// Gets or sets the query string
    /// </summary>
    public string? Query { get; set; }
}