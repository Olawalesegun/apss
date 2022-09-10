using APSS.Web.Mvc.Areas.Populatoin.Controllers;

namespace APSS.Web.Mvc.Util.Navigation.Routes;

public sealed class PopulationRoute : Route
{
    public PopulationRoute(IRoute parent) : base(parent, "Population Scan", "Population", icon: Icon.People)
    {
        Families = FromCrudController<FamiliesController>(icon: Icon.People);
        Individuals = FromCrudController<IndividualsController>(icon: Icon.People);
    }

    public CrudRoute Families { get; init; }
    public CrudRoute Individuals { get; init; }

    public override IRoute DefaultRoute => Families;
}