using System.ComponentModel.DataAnnotations;

namespace APSS.Web.Dtos.CustomValidation;

public class StartDateAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        DateTime _dateStart = Convert.ToDateTime(value);
        if (_dateStart >= DateTime.Now)
        {
            return ValidationResult.Success;
        }
        else
        {
            return new ValidationResult(ErrorMessage);
        }
    }
}