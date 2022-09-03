namespace APSS.Web.Mvc.Areas;

public static class Areas
{
    public const string Lands = "Lands";

    public static IEnumerable<string> All
    {
        get
        {
            yield return Lands;
        }
    }
}