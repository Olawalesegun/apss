using APSS.Web.Mvc.Areas.Lands.Controllers;

namespace APSS.Web.Mvc.Util.Navigation.Routes;

public sealed class LandsRoute : Route
{
    public LandsRoute(IRoute parent) : base(parent, "Lands", "Lands", icon: Icon.Mountain)
    {
        Lands = FromController<LandsController>(icon: Icon.Mountain);
        Products = FromController<ProductsController>(icon: Icon.Seeding);
        Units = FromController<UnitsController>(icon: Icon.Ruler);
        Seasons = FromController<SeasonsController>(icon: Icon.Calendar);
    }

    public IRoute Lands { get; init; }
    public IRoute Products { get; init; }
    public IRoute Units { get; init; }
    public IRoute Seasons { get; init; }

    public override IRoute DefaultRoute => Lands;
}