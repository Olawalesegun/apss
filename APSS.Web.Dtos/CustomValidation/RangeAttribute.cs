using System.ComponentModel.DataAnnotations;

namespace APSS.Web.Dtos.CustomValidation;

public class RangeAttribute : ValidationAttribute
{
    public double MaxValue { get; set; }
    public double MinValue { get; set; }

    public RangeAttribute(double minVal, double maxVal)
    {
        MaxValue = maxVal;
        MinValue = minVal;
    }

    public override bool IsValid(object value)
    {
        double inputVal;
        if (value == null)
            return false;

        if (double.TryParse(value.ToString(), out inputVal))
        {
            /*if (inputVal >= MinValue && inputVal <= MaxValue)
                return (inputVal % 1) == 0;
            else
                return false;*/
            if (inputVal >= MinValue && inputVal <= MaxValue)
                return true;
            else
                return false;
        }

        return false;
    }
}

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

public class MinValueF : ValidationAttribute
{
    private int _length;

    public MinValueF(int length)
    {
        this._length = length;
    }

    public override bool IsValid(object? value)
    {
        if (value == null)
            return value == null;

        string temp = value.ToString();
        if (temp.Length > _length)
            return true;

        return false;
    }
}