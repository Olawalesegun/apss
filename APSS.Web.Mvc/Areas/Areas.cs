namespace APSS.Web.Mvc.Areas;

public static class Areas
{
    public const string Auth = nameof(Auth);
    public const string Setup = nameof(Setup);
    public const string Surveys = "Surveys";
    public const string Animals = "Animals";
    public const string Users = "Users";

    public static IEnumerable<string> All
    {
        get
        {
            yield return Animals;
            yield return Lands;
            yield return Population;
            yield return Surveys;
            yield return Users;
        }
    }
}