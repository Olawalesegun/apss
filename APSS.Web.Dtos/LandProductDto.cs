namespace APSS.Web.Dtos;

public class LandProductDto : ProductDto
{
    public string StoringMethod { get; set; } = null!;
    public IEnumerable<LandProductUnitDto> Unit { get; set; } = new List<LandProductUnitDto>();

    public long UnitId { get; set; }
    public double Quantity { get; set; }
    public string CropName { get; set; } = null!;
    public string Category { get; set; } = null!;
    public bool HasGreenhouse { get; set; } = false;
    public string IrrigationMethod { get; set; } = null!;
    public double IrrigationCount { get; set; }
    public IrrigationWaterSourceDto IrrigationWaterSource { get; set; }
    public IrrigationPowerSourceDto IrrigationPowerSource { get; set; }
    public string Fertilizer { get; set; } = null!;
    public string Insecticide { get; set; } = null!;
    public DateTime HarvestStart { get; set; }
    public DateTime HarvestEnd { get; set; }
    public bool IsGovernmentFunded { get; set; }
    public LandDto Producer { get; set; } = null!;
    public long ProducerId { get; set; }
    public SeasonDto ProducedIn { get; set; } = null!;
    public long SeasonId { get; set; }
    public IEnumerable<SeasonDto> Seasons { get; set; } = new List<SeasonDto>();
}

public enum IrrigationWaterSourceDto
{
    Natural,
    HumanStored,
    OnDemand,
}

public enum IrrigationPowerSourceDto
{
    None,
    Renewable,
    FossileFuel,
}