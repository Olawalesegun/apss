namespace APSS.Web.Mvc.Util.Navigation;

public interface ICrudRoute : IRoute
{
    /// <summary>
    /// Gets the addition route
    /// </summary>
    IRoute Add { get; }

    /// <summary>
    /// Gets the detils route
    /// </summary>
    IRoute Details { get; }

    /// <summary>
    /// Gets the items updating route
    /// </summary>
    IRoute Update { get; }

    /// <summary>
    /// Gets the deletion route
    /// </summary>
    IRoute Delete { get; }
}