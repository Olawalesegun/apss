using APSS.Web.Mvc.Areas.Surveys.Controllers;

namespace APSS.Web.Mvc.Util.Navigation.Routes;

public sealed class SurveysRoute : Route
{
    public SurveysRoute(IRoute parent) : base(parent, "Surveys", "Surveys", icon: Icon.Poll)
    {
        Surveys = FromController<SurveysController>(icon: Icon.Poll);
        Entries = FromController<SurveyEntriesController>(icon: Icon.Poll);
    }

    public IRoute Surveys { get; init; }
    public IRoute Entries { get; init; }

    public override IRoute DefaultRoute => Surveys;
}