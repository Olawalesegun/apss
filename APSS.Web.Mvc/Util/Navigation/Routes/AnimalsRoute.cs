using APSS.Web.Mvc.Areas.Controllers;

namespace APSS.Web.Mvc.Util.Navigation.Routes;

public sealed class AnimalsRoute : Route
{
    public AnimalsRoute(IRoute parent) : base(parent, "Animals", "Animals", icon: Icon.Cow)
    {
        Groups = FromController<GroupsController>(icon: Icon.Cow);
        Products = FromController<ProductsController>(icon: Icon.Cow);
        Units = FromController<AnimalUnitsController>(icon: Icon.Cow);
    }

    public IRoute Groups { get; init; }
    public IRoute Products { get; init; }
    public IRoute Units { get; init; }

    public override IRoute DefaultRoute => Groups;
}