using APSS.Web.Mvc.Areas.Controllers;
using APSS.Web.Mvc.Areas.Users.Controllers;

namespace APSS.Web.Mvc.Util.Navigation.Routes;

public sealed class UsersRoute : CrudRoute
{
    public UsersRoute(IRoute parent) : base(parent, "User Managment", "Users", icon: Icon.Users)
    {
        Users = FromCrudController<UsersController>(icon: Icon.Key);
        Accounts = FromCrudController<AccountsController>(icon: Icon.Key);
    }

    public CrudRoute Users { get; init; }
    public CrudRoute Accounts { get; init; }

    public override IRoute DefaultRoute => Users;
}