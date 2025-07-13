using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace OpenTable.Models.Validations
{
    public class IsNotFutureDate : ValidationAttribute, IClientModelValidator
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime date)
            {
                if (date > DateTime.Today)
                {
                    return new ValidationResult(GetMsg(validationContext.DisplayName ?? "Date"));
                }
            }
            return ValidationResult.Success;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            if (!context.Attributes.ContainsKey("data-val"))
                context.Attributes.Add("data-val", "true");

            context.Attributes.Add("data-val-notfuturedate",
                GetMsg(context.ModelMetadata.DisplayName ?? context.ModelMetadata.Name ?? "Date"));
        }

        private string GetMsg(string name) =>
            ErrorMessage ?? $"{name} cannot be a future date.";
    }
}
