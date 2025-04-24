using System;
using System.ComponentModel.DataAnnotations;

public class GreaterThanZeroAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return new ValidationResult(ErrorMessage);
        }

        var numberValue = Convert.ToDouble(value);

        if (numberValue > 0)
        {
            return ValidationResult.Success;
        }
        return new ValidationResult(ErrorMessage ?? "The value must be greater than zero.");
    }
}