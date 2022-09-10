namespace APSS.Web.Mvc.Areas;

public static class Areas
{
    public const string Auth = nameof(Auth);
    public const string Setup = nameof(Setup);
    public const string Home = nameof(Home);
    public const string Animals = nameof(Animals);
    public const string Lands = nameof(Lands);
    public const string Population = nameof(Population);
    public const string Surveys = nameof(Surveys);
    public const string Users = nameof(Users);
    public const string Confirmations = nameof(Confirmations);

    public static IEnumerable<string> Dashboard
    {
        get
        {
            yield return Home;
            yield return Animals;
            yield return Lands;
            yield return Population;
            yield return Surveys;
            yield return Users;
            yield return Confirmations;
        }
    }
}