using APSS.Web.Mvc.Areas.Populatoin.Controllers;

namespace APSS.Web.Mvc.Util.Navigation.Routes;

public sealed class PopulationRoute : Route
{
    public PopulationRoute(IRoute parent) : base(parent, "Population", "Population", icon: Icon.People)
    {
        Families = FromController<FamiliesController>(icon: Icon.People);
        Individuals = FromController<IndividualsController>(icon: Icon.People);
    }

    public IRoute Families { get; init; }
    public IRoute Individuals { get; init; }

    public override IRoute DefaultRoute => Families;
}