using System.ComponentModel.DataAnnotations;
using APSS.Domain.Entities;

namespace APSS.Web.Dtos;

public class LandProductDto : ProductDto
{
    [Display(Name = "Storing Method")]
    [Required]
    [MinLength(2, ErrorMessage = "must be more than 1 digit")]
    public string StoringMethod { get; set; } = null!;

    [Display(Name = "Unit")]
    public LandProductUnitDto Unit { get; set; } = new LandProductUnitDto();

    public IEnumerable<LandProductUnitDto> Units { get; set; } = new List<LandProductUnitDto>();

    public long UnitId { get; set; }

    [Display(Name = "Quantity")]
    [Required(ErrorMessage = "Quantity is required")]
    [Range(1, double.MaxValue, ErrorMessage = " must be more than 0")]
    public double Quantity { get; set; }

    [Display(Name = "CropName ")]
    [Required]
    [MinLength(2, ErrorMessage = "must be more than 1 digit")]
    public string CropName { get; set; } = null!;

    [Display(Name = "Category")]
    [Required]
    [MinLength(2, ErrorMessage = "must be more than 1 digit")]
    public string Category { get; set; } = null!;

    [Display(Name = "Has Green house")]
    [Required]
    public bool HasGreenhouse { get; set; } = false;

    [Display(Name = "Irrigation Method")]
    [Required]
    [MinLength(2, ErrorMessage = "must be more than 1 digit")]
    public string IrrigationMethod { get; set; } = null!;

    [Display(Name = "Irrigation Count")]
    [Required]
    public double IrrigationCount { get; set; }

    [Display(Name = "Irrigation Water Source")]
    [Required]
    public IrrigationWaterSource IrrigationWaterSource { get; set; }

    [Display(Name = "Irrigation Power Source")]
    [Required]
    public IrrigationPowerSource IrrigationPowerSource { get; set; }

    [Display(Name = "Fertilizer")]
    [Required]
    [MinLength(2, ErrorMessage = "must be more than 1 digit")]
    public string Fertilizer { get; set; } = null!;

    [Display(Name = "Insecticide")]
    [Required]
    [MinLength(2, ErrorMessage = "must be more than 1 digit")]
    public string Insecticide { get; set; } = null!;

    [Display(Name = " Harvest Start")]
    [DataType(DataType.Date)]
    [Required]
    [CustomValidation.StartDate(ErrorMessage = "back date is not allowd")]
    public DateTime HarvestStart { get; set; }

    [Display(Name = "Harvest End")]
    [DataType(DataType.Date)]
    [Required]
    [CustomValidation.StartDate(ErrorMessage = "back date is not allowd")]
    public DateTime HarvestEnd { get; set; }

    [Display(Name = "Is Government Funded")]
    public bool IsGovernmentFunded { get; set; }

    [Display(Name = "Producer")]
    public LandDto Producer { get; set; } = null!;

    [Display(Name = "ProducedIn")]
    public SeasonDto ProducedIn { get; set; } = null!;

    public long SeasonId { get; set; }
    public long landId { get; set; }
    public IEnumerable<SeasonDto> Seasons { get; set; } = new List<SeasonDto>();
}