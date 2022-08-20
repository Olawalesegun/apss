namespace APSS.Web.Dtos;

public class LandProductAndSeasonAndUnit
{
    public LandProductDto Product { get; set; } = new LandProductDto();

    public IEnumerable<SeasonDto> Seasons { get; set; } = new List<SeasonDto>();
    public SeasonDto SeasonId { get; set; } = new SeasonDto();
    public IEnumerable<LandProductUnitDto> ProductUnits { get; set; } = new List<LandProductUnitDto>();
    public LandProductUnitDto ProductUnitId { get; set; } = new LandProductUnitDto();
}