namespace APSS.Web.Mvc.Util.Navigation.Routes;

public sealed class AnimalsRoute : Route
{
    public AnimalsRoute(IRoute parent) : base(parent, "Animals", "Animals", icon: Icon.Cow)
    {
        Groups = new Route(this, "Animal Groups", "Groups", icon: Icon.Cow);
        Products = new Route(this, "Animal Products", "Products", icon: Icon.Cheese);
        Units = new Route(this, "Animal Product Units", "Units", icon: Icon.Scale);
    }

    public IRoute Groups { get; init; }
    public IRoute Products { get; init; }
    public IRoute Units { get; init; }
}