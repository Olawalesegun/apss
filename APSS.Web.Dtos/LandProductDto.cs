using System.ComponentModel.DataAnnotations;

namespace APSS.Web.Dtos;

public class LandProductDto : ProductDto
{
    [Display(Name = "طريقة التخزين")]
    public string StoringMethod { get; set; } = null!;

    [Display(Name = "وحدة المنتج")]
    public LandProductUnitDto Unit { get; set; } = new LandProductUnitDto();

    public IEnumerable<LandProductUnitDto> Units { get; set; } = new List<LandProductUnitDto>();

    public long UnitId { get; set; }

    [Display(Name = "الكمية")]
    public double Quantity { get; set; }

    [Display(Name = "اسم المحصول")]
    public string CropName { get; set; } = null!;

    [Display(Name = "صنف المحصول")]
    public string Category { get; set; } = null!;

    [Display(Name = "لديها مشتل ؟")]
    public bool HasGreenhouse { get; set; } = false;

    [Display(Name = "طريقة الري")]
    public string IrrigationMethod { get; set; } = null!;

    [Display(Name = "عدد مرات الري")]
    public double IrrigationCount { get; set; }

    [Display(Name = "مصدر المياه")]
    public IrrigationWaterSourceDto IrrigationWaterSource { get; set; }

    [Display(Name = "مصدر الطاقة")]
    public IrrigationPowerSourceDto IrrigationPowerSource { get; set; }

    [Display(Name = "السماد")]
    public string Fertilizer { get; set; } = null!;

    [Display(Name = "المبيدات")]
    public string Insecticide { get; set; } = null!;

    [Display(Name = "تاريخ البداية")]
    public DateTime HarvestStart { get; set; }

    [Display(Name = "تاريخ الحصاد")]
    public DateTime HarvestEnd { get; set; }

    [Display(Name = "ممول من الحكومة؟")]
    public bool IsGovernmentFunded { get; set; }

    [Display(Name = "الارض المنتجة")]
    public LandDto Producer { get; set; } = null!;

    public long ProducerId { get; set; }

    [Display(Name = "موسم الانتاج")]
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