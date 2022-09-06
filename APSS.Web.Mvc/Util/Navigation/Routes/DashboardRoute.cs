using APSS.Web.Mvc.Areas.Home.Controllers;
using APSS.Web.Mvc.Areas.Surveys.Controllers;

namespace APSS.Web.Mvc.Util.Navigation.Routes;

public sealed class DashboardRoute : Route
{
    public DashboardRoute(IRoute parent) : base(parent, "Dashboard", string.Empty)
    {
        Home = FromController<HomeController>(icon: Icon.Home);
        Users = new UsersRoute(this);
        Animals = new AnimalsRoute(this);
        Lands = new LandsRoute(this);
        Surveys = FromController<SurveysController>(icon: Icon.Poll);
        Population = new PopulationRoute(this);
        Settings = new Route(this, "Application Settings", "Settings", icon: Icon.Gear);
    }

    public IRoute Home { get; init; }
    public UsersRoute Users { get; init; }
    public AnimalsRoute Animals { get; init; }
    public LandsRoute Lands { get; init; }
    public IRoute Surveys { get; init; }
    public PopulationRoute Population { get; init; }
    public IRoute Settings { get; init; }

    public override IRoute DefaultRoute => Home;
}