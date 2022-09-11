using APSS.Web.Mvc.Areas.Animals.Controllers;
using APSS.Web.Mvc.Areas.Controllers;

namespace APSS.Web.Mvc.Util.Navigation.Routes;

public sealed class AnimalsRoute : Route
{
    public AnimalsRoute(IRoute parent) : base(parent, "Animal Managment", "Animals", icon: Icon.Cow)
    {
        Groups = FromCrudController<GroupsController>(icon: Icon.Cow);
        Products = FromCrudController<ProductsController>(icon: Icon.Cow);
        Units = FromCrudController<UnitsController>(icon: Icon.Cow);
        Expense = FromCrudController<ExpensesController>(icon: Icon.Cow);
        Confirmation = FromCrudController<ConfirmationsController>(icon: Icon.People);
    }

    public CrudRoute Groups { get; init; }
    public CrudRoute Products { get; init; }
    public CrudRoute Units { get; init; }
    public CrudRoute Expense { get; init; }
    public CrudRoute Confirmation { get; init; }

    public override IRoute DefaultRoute => Groups;
}