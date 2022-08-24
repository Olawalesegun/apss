using System.ComponentModel.DataAnnotations;

namespace APSS.Web.Dtos;

public class LandDto : OwnableDto
{
    [Display(Name = "مساحة الارض")]
    public long Area { get; set; }

    [Display(Name = "خط الطول")]
    public double Longitude { get; set; }

    [Display(Name = "خط العرض")]
    public double Latitude { get; set; }

    [Display(Name = "العنوان")]
    public string Address { get; set; } = null!;

    [Display(Name = "قابلية الزراعة")]
    public bool IsUsable { get; set; }

    [Display(Name = "مزروعة؟ ")]
    public bool IsUsed { get; set; } = false;

    [Display(Name = "منتجات الارض")]
    public ICollection<LandProductDto> Products { get; set; } = new List<LandProductDto>();
}