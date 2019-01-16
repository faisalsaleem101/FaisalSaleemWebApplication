using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FaisalLearningProjectMVC.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class GenericRequiredAttribute : ValidationAttribute, IClientModelValidator
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var text = value?.ToString() ?? "";

            if (string.IsNullOrEmpty(text))
            {
                var errorMessage = $"Please enter your {validationContext.DisplayName}";
                return new ValidationResult(errorMessage);
            }

            return ValidationResult.Success;
        }


        public void AddValidation(ClientModelValidationContext context)
        {
            MergeAttribute(context.Attributes, "data-val", "true");
            var errorMessage = $"Please enter your {context.ModelMetadata.GetDisplayName() }";
            MergeAttribute(context.Attributes, "data-val-genericrequired", errorMessage);
        }

        private bool MergeAttribute( IDictionary<string, string> attributes, string key, string value)
        {
            if (attributes.ContainsKey(key))
            {
                return false;
            }
            attributes.Add(key, value);
            return true;
        }
    }
}
