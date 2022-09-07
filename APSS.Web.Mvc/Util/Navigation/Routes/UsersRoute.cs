using APSS.Web.Mvc.Areas.Controllers;

namespace APSS.Web.Mvc.Util.Navigation.Routes;

public sealed class UsersRoute : Route
{
    public UsersRoute(IRoute parent) : base(parent, "Users", "Users", icon: Icon.Users)
    {
        Accounts = FromController<AccountsController>(icon: Icon.Key);
    }

    public IRoute Accounts { get; init; }
}