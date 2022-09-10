namespace APSS.Web.Mvc.Util.Navigation;

public class CrudRoute : Route, ICrudRoute
{
    public CrudRoute(
        IRoute? parent,
        string name,
        string? pathSegment = null,
        IList<IRoute>? children = null,
        Icon icon = Icon.None) : base(parent, name, pathSegment, children, icon)
    {
        Add = new Route(this, "Add item", "Add", icon: Icon.Plus, isNavigatable: false);
        Update = new Route(this, "Update item", "Update", icon: Icon.Pen, isNavigatable: false);
        Delete = new Route(this, "Delete item", "Delete", icon: Icon.TrashCan, isNavigatable: false);
        DeletePost = new Route(this, "Delete item", "DeletePost", icon: Icon.TrashCan, isNavigatable: false);
        Details = new Route(this, "Show details", "Details", icon: Icon.List, isNavigatable: false);
        GetAll = new Route(this, "Show details", "GetAll", icon: Icon.List, isNavigatable: false);
    }

    /// <inheritdoc/>
    public IRoute Add { get; init; }

    /// <inheritdoc/>
    public IRoute Details { get; init; }

    /// <inheritdoc/>
    public IRoute Update { get; init; }

    /// <inheritdoc/>
    public IRoute Delete { get; init; }

    public IRoute DeletePost { get; init; }

    public IRoute GetAll { get; init; }
}