using APSS.Web.Mvc.Areas.Users.Controllers;

namespace APSS.Web.Mvc.Util.Navigation.Routes;

public sealed class UserManagmentRoute : CrudRoute
{
    public UserManagmentRoute(IRoute parent) : base(parent, "User Managment", "Users", icon: Icon.Users)
    {
        Users = new UsersRoute(this);
        Accounts = new AccountsRoute(this);
    }

    public UsersRoute Users { get; init; }
    public AccountsRoute Accounts { get; init; }

    public override IRoute DefaultRoute => Users;
}

public sealed class UsersRoute : CrudRoute
{
    public UsersRoute(IRoute? parent) : base(parent, "Users", "Users", icon: Icon.Users)
    {
        SubUsers = new Route(this, "Sub-users", "SubUsers");
    }

    public IRoute SubUsers { get; init; }
}

public sealed class AccountsRoute : CrudRoute
{
    public AccountsRoute(IRoute? parent) : base(parent, "Accounts", "Accounts", icon: Icon.Key)
    {
        UpdatePassword = new Route(this, "Update Password", "UpdatePassword");
    }

    public IRoute UpdatePassword { get; init; }
}