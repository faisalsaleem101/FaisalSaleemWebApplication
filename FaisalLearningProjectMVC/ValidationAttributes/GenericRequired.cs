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

        public override bool IsValid(object value)
        {
            var message = value as string;

            if (string.IsNullOrEmpty(message))
            {
                //ErrorMessage = $"Please enter your {context.ModelMetadata.GetDisplayName() }";
                return false;
            }

            return true;
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
