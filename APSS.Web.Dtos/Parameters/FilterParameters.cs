using System.Linq.Expressions;

namespace APSS.Web.Dtos.Parameters;

public class FilterParameters<T>
{
    /// <summary>
    /// Gets or sets the set of fields to filter by. A null value means that all fields are going to
    /// be used in filtering
    /// </summary>
    public T Fields { get; init; } = default!;

    /// <summary>
    /// Gets or sets the query string to filter by
    /// </summary>
    public string? Query { get; set; }
    }

public class FilterParameters
{
    /// <summary>
    /// Gets or sets the set of fields to filter by. A null value means that all fields are going to
    /// be used in filtering
    /// </summary>
    public IEnumerable<string>? Fields { get; set; }

    /// <summary>
    /// Gets or sets the query string to filter by
    /// </summary>
    public string? Query { get; set; }

    /// <summary>
    /// Gets a strongly typed version of the filter using reflection
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public FilterParameters<T> IntoStronglyTyped<T>() where T : new()
    {
        var ret = new FilterParameters<T>();

        if (Fields is null)
            return ret;

        foreach (var prop in typeof(T).GetProperties().Where(p => p.PropertyType == typeof(bool)))
        {
            var propName = prop.Name;

            if (Fields.Any(f => f.Equals(prop.Name, StringComparison.InvariantCultureIgnoreCase)))
                prop.SetValue(ret, true);
        }

        return new FilterParameters<T>();
    }
}