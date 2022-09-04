namespace APSS.Web.Mvc.Util.Navigation.Routes;

public sealed class PopulationRoute : Route
{
    public PopulationRoute(IRoute parent) : base(parent, "Population", "Population", icon: Icon.People)
    {
        Families = new Route(this, "Families", "Families", icon: Icon.People);
        Individuals = new Route(this, "Individuals", "Individuals", icon: Icon.People);
    }

    public IRoute Families { get; init; }
    public IRoute Individuals { get; init; }

    public override IRoute DefaultRoute => Families;
}