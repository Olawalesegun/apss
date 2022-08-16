namespace APSS.Web.Mvc.Models
{
    public class AnimalGroup
    {
        public int Id { get; set; }
        public string Type { get; set; } = null!;

        public int Quantity { get; set; }

        public string? Sex { get; set; }
    }
}