using System;
using System.ComponentModel.DataAnnotations;

namespace AstronautSatelliteAPI.Validation;

public class PastDateAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is DateTime date && date >= DateTime.Now)
        {
            return new ValidationResult("The date must be in the past.");
        }
        return ValidationResult.Success;
    }
}