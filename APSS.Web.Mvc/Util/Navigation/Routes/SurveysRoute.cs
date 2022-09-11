using APSS.Web.Mvc.Areas.Surveys.Controllers;

namespace APSS.Web.Mvc.Util.Navigation.Routes;

public sealed class SurveysRoute : Route
{
    public SurveysRoute(IRoute parent) : base(parent, "Survey Managment", "Surveys", icon: Icon.Poll)
    {
        Surveys = FromCrudController<SurveysController>(icon: Icon.Poll);
        Entries = FromCrudController<SurveyEntriesController>(icon: Icon.Poll);
        Questions = FromCrudController<QuestionsController>(icon: Icon.Poll);
    }

    public CrudRoute Surveys { get; init; }
    public CrudRoute Entries { get; init; }
    public CrudRoute Questions { get; init; }

    public override IRoute DefaultRoute => Surveys;
}