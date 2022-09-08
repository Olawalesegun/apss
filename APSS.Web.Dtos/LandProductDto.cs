﻿using System.ComponentModel.DataAnnotations;
using APSS.Domain.Entities;

namespace APSS.Web.Dtos;

public class LandProductDto : ProductDto
{
    [Display(Name = "Storing Method")]
    [MinLength(2, ErrorMessage = "must be more than 2 digit")]
    [MaxLength(8, ErrorMessage = "must be les than 9 digit")]
    public string StoringMethod { get; set; } = null!;

    [Display(Name = "Unit")]
    public LandProductUnitDto Unit { get; set; } = new LandProductUnitDto();

    public IEnumerable<LandProductUnitDto> Units { get; set; } = new List<LandProductUnitDto>();

    public long UnitId { get; set; }

    [Display(Name = "Quantity")]
    [Required(ErrorMessage = "Quantity is required")]
    [Range(1, double.MaxValue, ErrorMessage = " must be more than 1")]
    public double Quantity { get; set; }

    [Display(Name = "CropName ")]
    public string CropName { get; set; } = null!;

    [Display(Name = "Category")]
    public string Category { get; set; } = null!;

    [Display(Name = "Has Green house")]
    public bool HasGreenhouse { get; set; } = false;

    [Display(Name = "Irrigation Method")]
    public string IrrigationMethod { get; set; } = null!;

    [Display(Name = "Irrigation Count")]
    public double IrrigationCount { get; set; }

    [Display(Name = "Irrigation Water Source")]
    public IrrigationWaterSource IrrigationWaterSource { get; set; }

    [Display(Name = "Irrigation Power Source")]
    public IrrigationPowerSource IrrigationPowerSource { get; set; }

    [Display(Name = "Fertilizer")]
    public string Fertilizer { get; set; } = null!;

    [Display(Name = "Insecticide")]
    public string Insecticide { get; set; } = null!;

    [Display(Name = " Harvest Start")]
    [DataType(DataType.Date)]
    [Required(ErrorMessage = "Required")]
    [CustomValidation.StartDate(ErrorMessage = "back date is not allowd")]
    public DateTime HarvestStart { get; set; }

    [Display(Name = "Harvest End")]
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