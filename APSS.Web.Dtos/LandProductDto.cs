namespace APSS.Web.Dtos;

public class LandProductDto : ProductDto
{
    public string StoringMethod { get; set; } = null!;
    public LandProductUnitDto Unit { get; set; } = null!;
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
    public SeasonDto ProducedIn { get; set; } = null!;
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