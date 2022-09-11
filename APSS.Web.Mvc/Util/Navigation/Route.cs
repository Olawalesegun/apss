namespace APSS.Web.Mvc.Util.Navigation;

public class Route : IRoute
{
    #region Fields

    private readonly IList<IRoute> _children;
    private readonly string _fullPath;
    private readonly Icon _icon;
    private readonly bool _isNavigatable;
    private readonly string _name;
    private readonly IRoute? _parent;
    private readonly string _pathSegment;

    #endregion Fields

    #region Public Constructors

    public Route(
        IRoute? parent,
        string name,
        string? pathSegment = null,
        IList<IRoute>? children = null,
        Icon icon = Icon.None,
        bool isNavigatable = true)
    {
        if (parent is not null)
            parent.Children.Add(this);

        _parent = parent;
        _name = name;
        _icon = icon;
        _pathSegment = pathSegment ?? string.Empty;
        _children = children ?? new List<IRoute>();

        IList<string> segments = new List<string>();

        for (IRoute? segment = this; segment != null; segment = segment.Parent)
        {
            if (segment.PathSegment.Length > 0)
                segments.Add(segment.PathSegment);
        }

        _fullPath = "/" + string.Join('/', segments.Reverse());
        _isNavigatable = isNavigatable;
    }

    #endregion Public Constructors

    #region Properties

    /// <inheritdoc/>
    public IList<IRoute> Children => _children;

    /// <inheritdoc/>
    public virtual IRoute DefaultRoute => this;

    /// <inheritdoc/>
    public string FullPath => _fullPath;

    /// <inheritdoc/>
    public Icon Icon => _icon;

    /// <inheritdoc/>
    public bool IsNavigatable => _isNavigatable;

    /// <inheritdoc/>
    public string Name => _name;

    /// <inheritdoc/>
    public IRoute? Parent => _parent;

    /// <inheritdoc/>
    public string PathSegment => _pathSegment;

    #endregion Properties

    #region Public Methods

    /// <inheritdoc/>
    public override string ToString() => FullPath;

    #endregion Public Methods

    #region Protected Methods

    protected IRoute FromController<T>(
        string? displayName = null,
        IList<IRoute>? children = null,
        Icon icon = Icon.None)
    {
        var baseName = typeof(T).Name.Replace("Controller", string.Empty);

        return new Route(this, displayName ?? baseName, baseName, children, icon);
    }

    protected CrudRoute FromCrudController<T>(
        string? displayName = null,
        Icon icon = Icon.None,
        bool isNavigatable = true)
    {
        var baseName = typeof(T).Name.Replace("Controller", string.Empty);

        return new CrudRoute(this, displayName ?? baseName, baseName, icon: icon, isNavigatable: isNavigatable);
    }

    #endregion Protected Methods
}