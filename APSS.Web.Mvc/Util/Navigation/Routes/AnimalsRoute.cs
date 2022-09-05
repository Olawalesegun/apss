using APSS.Web.Mvc.Areas.Controllers;

namespace APSS.Web.Mvc.Util.Navigation.Routes;

public sealed class AnimalsRoute : Route
{
    public AnimalsRoute(IRoute parent) : base(parent, "Animals", "Animals", icon: Icon.Cow)
    {
        Groups = FromController<Controllers.GroupsController>(icon: Icon.Cow);
        Products = FromController<AnimalProductsController>(icon: Icon.Cow);
        Units = FromController<Controllers.AnimalUnitsController>(icon: Icon.Cow);
    }

    public IRoute Groups { get; init; }
    public IRoute Products { get; init; }
    public IRoute Units { get; init; }
}