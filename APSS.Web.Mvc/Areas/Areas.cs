namespace APSS.Web.Mvc.Areas;

public static class Areas
{
    public const string Lands = "Lands";
    public const string Population = "Population";
    public const string Surveys = "Surveys";
    public const string Animals = "Animals";
    public const string AnimalProducts = "AnimalProducts";
    public const string Accounts = "Accounts";

    public static IEnumerable<string> All
    {
        get
        {
            yield return Animals;
            yield return Lands;
            yield return Population;
            yield return Surveys;
            yield return AnimalProducts;
            yield return Accounts;
        }
    }
}