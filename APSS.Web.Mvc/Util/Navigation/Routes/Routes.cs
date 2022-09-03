using Microsoft.AspNetCore.Routing;

namespace APSS.Web.Mvc.Util.Navigation.Routes;

public static class Routes
{
    private static readonly IRoute Root = new Route(null, string.Empty);

    public static AuthRoute Auth { get; } = new AuthRoute(Root);
    public static DashboardRoute Dashboard { get; } = new DashboardRoute(Root);
    public static IDictionary<string, IRoute> All { get; } = new Dictionary<string, IRoute>(GenerateRoutes(Root));

    private static IEnumerable<KeyValuePair<string, IRoute>> GenerateRoutes(IRoute root)
    {
        foreach (var child in root.Children)
        {
            yield return new(child.FullPath, child);

            if (child.Children.Count > 0)
            {
                foreach (var route in GenerateRoutes(child))
                    yield return route;
            }
        }
    }
}