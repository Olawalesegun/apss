namespace APSS.Web.Mvc.Util.Navigation;

public interface IRoute
{
    /// <summary>
    /// Gets the name of the route
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets the icon of the route
    /// </summary>
    public Icon Icon { get; }

    /// <summary>
    /// Gets the path of the route
    /// </summary>
    public string PathSegment { get; }

    /// <summary>
    /// Gets the full path of this route
    /// </summary>
    public string FullPath { get; }

    /// <summary>
    /// Gets the parent route of this route
    /// </summary>
    public IRoute? Parent { get; }

    /// <summary>
    /// Gets the children routes of this route
    /// </summary>
    public IList<IRoute> Children { get; }

    /// <summary>
    /// Gets the default route of this route
    /// </summary>
    public IRoute DefaultRoute { get; }

    /// <summary>
    /// Gets the route heirarchy
    /// </summary>
    public IEnumerable<IRoute> Heirarchy
    {
        get
        {
            IRoute? route = this;

            while (route is not null)
            {
                yield return route;

                route = route.Parent;
            }
        }
    }
}