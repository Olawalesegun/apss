using APSS.Web.Mvc.Areas.Lands.Controllers;

namespace APSS.Web.Mvc.Util.Navigation.Routes;

public sealed class LandsRoute : CrudRoute
{
    public LandsRoute(IRoute parent) : base(parent, "Land Managment", "Lands", icon: Icon.Mountain)
    {
        Lands = FromCrudController<LandsController>(icon: Icon.Seeding);
        Products = FromCrudController<ProductsController>(icon: Icon.Seeding);
        Units = FromCrudController<UnitsController>(icon: Icon.Ruler);
        Seasons = FromCrudController<SeasonsController>(icon: Icon.Calendar);
    }

    public CrudRoute Lands { get; init; }
    public CrudRoute Products { get; init; }
    public CrudRoute Units { get; init; }
    public CrudRoute Seasons { get; init; }

    public override IRoute DefaultRoute => Lands;
}