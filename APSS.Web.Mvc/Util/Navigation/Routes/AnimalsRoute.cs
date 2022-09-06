using APSS.Web.Mvc.Areas.Controllers;
using APSS.Web.Mvc.Controllers;

namespace APSS.Web.Mvc.Util.Navigation.Routes;

public sealed class AnimalsRoute : Route
{
    public AnimalsRoute(IRoute parent) : base(parent, "Animals", "Animals", icon: Icon.Cow)
    {
        Groups = FromController<GroupsController>(icon: Icon.Cow);
        Products = FromController<ProductsController>(icon: Icon.Cow);
        Units = FromController<AnimalUnitsController>(icon: Icon.Cow);
        Expense = FromController<ExpensesController>(icon: Icon.Cow);
        Confirmation = FromController<ConfirmationsController>(icon: Icon.People);
    }

    public IRoute Groups { get; init; }
    public IRoute Products { get; init; }
    public IRoute Units { get; init; }
    public IRoute Expense { get; init; }
    public IRoute Confirmation { get; init; }

    public override IRoute DefaultRoute => Groups;
}