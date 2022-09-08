using System.Collections;

using APSS.Web.Dtos.Parameters;

namespace APSS.Web.Mvc.Models;

public sealed class CrudViewModel<T> : IEnumerable<T>
{
    private readonly IEnumerable<T> _stream;
    private readonly FilteringParameters _args;

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="stream"></param>
    public CrudViewModel(IEnumerable<T> stream, FilteringParameters args)
    {
        _stream = stream;
        _args = args;
    }

    /// <summary>
    /// Gets or sets filtering arguments
    /// </summary>
    public FilteringParameters Params => _args;

    /// <inheritdoc/>
    public IEnumerator<T> GetEnumerator()
        => _stream.GetEnumerator();

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator()
        => _stream.GetEnumerator();
}