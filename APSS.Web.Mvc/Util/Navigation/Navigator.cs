using Microsoft.AspNetCore.Mvc;
using APSS.Web.Mvc.Util.Navigation;
using APSS.Web.Mvc.Util.Navigation.Routes;

public static class Navigator
{
    public static IRoute? GetCurrentRoute(this HttpContext context)
    {
        var path = context.Request.Path;

        return Routes.All.ContainsKey(path) ? Routes.All[path] : null;
    }

    public static IRoute? GetCurrentDashboardRoute(this HttpContext context)
    {
        var path = context.Request.Path.Value ?? "/";

        return Routes.Dashboard.Children.FirstOrDefault(r => path.StartsWith(r.FullPath));
    }

    public static void NavigateTo(this HttpContext self, IRoute route)
        => self.Response.Redirect(route.FullPath);
}