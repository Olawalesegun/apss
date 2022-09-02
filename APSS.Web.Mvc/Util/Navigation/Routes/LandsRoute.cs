namespace APSS.Web.Mvc.Util.Navigation.Routes;

public sealed class LandsRoute : Route
{
    public LandsRoute(IRoute parent) : base(parent, "Lands", "Lands", icon: Icon.Mountain)
    {
        Lands = new Route(this, "Lands", "Lands", icon: Icon.Mountain);
        Products = new Route(this, "Land Products", "Products", icon: Icon.Seeding);
        Units = new Route(this, "Land Product Units", "Units", icon: Icon.Ruler);
    }

    public IRoute Lands { get; init; }
    public IRoute Products { get; init; }
    public IRoute Units { get; init; }

    public override IRoute DefaultRoute => Lands;
}