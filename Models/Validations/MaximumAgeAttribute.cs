using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace OpenTable.Models.Validations
{
    public class MaximumAgeAttribute : ValidationAttribute, IClientModelValidator
    {
        private int maxYears;
        public MaximumAgeAttribute(int years)
        {
            maxYears = years;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext ctx)
        {
            if (value is DateTime dob)
            {
                int age = DateTime.Today.Year - dob.Year;
                if (dob.Date > DateTime.Today.AddYears(-age)) age--;

                if (age <= maxYears)
                {
                    return ValidationResult.Success;
                }
            }

            return new ValidationResult(GetMsg(ctx.DisplayName ?? "Date"));
        }

        public void AddValidation(ClientModelValidationContext ctx)
        {
            if (!ctx.Attributes.ContainsKey("data-val"))
                ctx.Attributes.Add("data-val", "true");
            ctx.Attributes.Add("data-val-maximumage-years",
                maxYears.ToString());
            ctx.Attributes.Add("data-val-maximumage",
                GetMsg(ctx.ModelMetadata.DisplayName ?? ctx.ModelMetadata.Name ?? "Date"));
        }

        private string GetMsg(string name) =>
            ErrorMessage ?? $"{name} must not be at greater than {maxYears} years.";
    }
}
