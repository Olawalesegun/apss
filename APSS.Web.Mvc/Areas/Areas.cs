namespace APSS.Web.Mvc.Areas;

public static class Areas
{
    public const string Lands = "Lands";
    public const string Population = "Population";
    public const string Surveys = "Surveys";

    public static IEnumerable<string> All
    {
        get
        {
            yield return Lands;
            yield return Population;
            yield return Surveys;
        }
    }
}