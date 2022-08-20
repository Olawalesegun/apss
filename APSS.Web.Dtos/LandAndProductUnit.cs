namespace APSS.Web.Dtos;

public class LandAndProductUnit
{
    public IEnumerable<LandDto> LandList { get; set; }  = new List<LandDto>();
    public LandDto landDto { get; set; } = new LandDto();

    public IEnumerable<LandProductUnitDto> ProductUnitList { get; set; } = new List<LandProductUnitDto>();
    public LandProductUnitDto ProductUnitDto { get; set; } = new LandProductUnitDto();
}