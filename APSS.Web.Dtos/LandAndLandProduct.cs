namespace APSS.Web.Dtos;

public class LandAndLandProduct
{
    public IEnumerable<LandDto> LandList { get; set; } = new List<LandDto>();
    public LandDto landDto { get; set; } = new LandDto();

    public IEnumerable<LandProductDto> ProductList { get; set; } = new List<LandProductDto>();
    public LandProductDto landProductDto { get; set; } = new LandProductDto();
}