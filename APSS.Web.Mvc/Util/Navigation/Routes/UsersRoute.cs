using APSS.Web.Mvc.Areas.Controllers;

namespace APSS.Web.Mvc.Util.Navigation.Routes;

public sealed class UsersRoute : Route
{
    public UsersRoute(IRoute parent) : base(parent, "Users Managment", "Users", icon: Icon.Users)
    {
        /* Test1 = new Route(this, "Sign In", "#test1");
         Test2 = new Route(this, "Sign Out", "#test2");*/
        Users = FromController<UsersController>(icon: Icon.Key);
        Accounts = FromController<AccountsController>(icon: Icon.Key);
    }

    public IRoute Accounts { get; init; }
    public IRoute Users { get; init; }

    public override IRoute DefaultRoute => Users;
}