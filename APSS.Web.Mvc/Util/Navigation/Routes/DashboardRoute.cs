using APSS.Domain.Entities;
using APSS.Web.Mvc.Controllers;

namespace APSS.Web.Mvc.Util.Navigation.Routes;

public sealed class DashboardRoute : Route
{
    public DashboardRoute(IRoute parent) : base(parent, "Dashboard", string.Empty)
    {
        Home = FromController<HomeController>(icon: Icon.Home);
        Users = new UsersRoute(this);
        Accounts = FromController<AccountsController>(icon: Icon.Key);
        Animals = new AnimalsRoute(this);
        Lands = new LandsRoute(this);
        Surveys = FromController<SurveysController>(icon: Icon.Poll);
        Population = new PopulationRoute(this);
        Settings = new Route(this, "Application Settings", "Settings", icon: Icon.Gear);
    }

    public IRoute Home { get; init; }
    public IRoute Users { get; init; }
    public IRoute Accounts { get; init; }
    public IRoute Animals { get; init; }
    public IRoute Lands { get; init; }
    public IRoute Surveys { get; init; }
    public IRoute Population { get; init; }
    public IRoute Settings { get; init; }

    public override IRoute DefaultRoute => Home;
}