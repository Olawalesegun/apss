using System.ComponentModel.DataAnnotations;

namespace APSS.Web.Dtos;

public abstract class BaseDto
{
    [Display(Name = "ID")]
    public long Id { get; set; }

    [Display(Name = "Date Of Create")]
    public DateTime CreatedAt { get; set; }
}