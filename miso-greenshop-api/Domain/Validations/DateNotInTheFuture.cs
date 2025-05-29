﻿using System.ComponentModel.DataAnnotations;

namespace miso_greenshop_api.Domain.Validations
{
    public class DateNotInTheFuture : ValidationAttribute
    {
        protected override ValidationResult IsValid(
            object? value, 
            ValidationContext validationContext)
        {
            if(value == null)
            {
                return new ValidationResult(
                    "Value for Date must be provided.");
            }
            var dateValue = value as DateTime? ?? 
                new DateTime();

            if (dateValue.Date > DateTime.Now.Date)
            {
                return new ValidationResult(
                    "Date cannot be in the future.");
            }

            return ValidationResult.Success!;
        }
    }
}
