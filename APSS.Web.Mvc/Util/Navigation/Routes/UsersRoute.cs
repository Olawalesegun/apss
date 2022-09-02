namespace APSS.Web.Mvc.Util.Navigation.Routes;

public sealed class UsersRoute : Route
{
    public UsersRoute(IRoute parent) : base(parent, "Users Managment", "Users")
    {
        Test1 = new Route(this, "Sign In", "#test1");
        Test2 = new Route(this, "Sign Out", "#test2");
    }

    public IRoute Test1 { get; init; }
    public IRoute Test2 { get; init; }

    public override IRoute DefaultRoute => Test1;
}