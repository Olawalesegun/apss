﻿using APSS.Web.Mvc.Areas.Lands.Controllers;

namespace APSS.Web.Mvc.Util.Navigation.Routes;

public sealed class LandsRoute : CrudRoute
{
    public LandsRoute(IRoute parent) : base(parent, "Lands", "Lands", icon: Icon.Mountain)
    {
        Products = FromCrudController<ProductsController>(icon: Icon.Seeding);
        Units = FromCrudController<UnitsController>(icon: Icon.Ruler);
        Seasons = FromCrudController<SeasonsController>(icon: Icon.Calendar);
    }

    public CrudRoute Products { get; init; }
    public CrudRoute Units { get; init; }
    public CrudRoute Seasons { get; init; }
}