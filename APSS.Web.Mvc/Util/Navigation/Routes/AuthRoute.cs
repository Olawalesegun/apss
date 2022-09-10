using APSS.Web.Mvc.Areas.Auth.Controllers;
using APSS.Web.Mvc.Areas.Controllers;

namespace APSS.Web.Mvc.Util.Navigation.Routes;

public sealed class AuthRoute : Route
{
    public AuthRoute(IRoute parent) : base(parent, "Authentication", "Auth")
    {
        SignIn = new Route(this, "Sign In", nameof(AuthController.Login));
        SignOut = new Route(this, "Sign Out", nameof(AuthController.Logout));
    }

    public IRoute SignIn { get; init; }
    public IRoute SignOut { get; init; }

    public override IRoute DefaultRoute => SignIn;
}