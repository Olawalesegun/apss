namespace APSS.Web.Dtos;

public class LandDto : OwnableDto
{
    public long Area { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
    public string Address { get; set; } = null!;
    public bool IsUsable { get; set; }
    public bool IsUsed { get; set; } = false;
    public ICollection<LandProductDto> Products { get; set; } = new List<LandProductDto>();
}