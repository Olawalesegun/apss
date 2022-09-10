namespace APSS.Web.Dtos;

public class LandConfirmationDto
{
    public IEnumerable<LandDto> Lands { get; set; } = new List<LandDto>();
    public IEnumerable<LandProductDto> Products { get; set; } = new List<LandProductDto>();
    public LandDto Land { get; set; } = new LandDto();
    public LandProductDto Product { get; set; } = new LandProductDto();
}