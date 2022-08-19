namespace APSS.Web.Mvc.Models
{
    public class AnimalGroup
    {
        public int Id { get; set; }
        public string Type { get; set; } = null!;

        public int Quantity { get; set; } = 0;

        public string Sex { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}