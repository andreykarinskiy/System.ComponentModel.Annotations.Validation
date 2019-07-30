using PropertyProvider;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using PropertyProvider.Accessor;

namespace System.ComponentModel.Annotations.Validation.PropertyValidator
{
    /// <inheritdoc cref="IPropertyValidator"/>
    public sealed class DefaultPropertyValidator : IPropertyValidator
    {
        #region Implementation of IPropertyValidator

        /// <inheritdoc cref="IPropertyValidator.Validate"/>
        public IEnumerable<ValidationResult> Validate(object source, IPropertyAccessor property, IPropertyAccessor parentProperty)
        {
            var propertyValue = property.GetValue(source);

            if (HasRequiredAttribute(property, out var requiredValidator))
            {
                var result = Validate(requiredValidator, property.Name, propertyValue, parentProperty?.Name);

                yield return result;

                yield break;
            }

            foreach (var validator in property.Attributes.OfType<ValidationAttribute>())
            {
                var result = Validate(validator, property.Name, propertyValue, parentProperty?.Name);

                yield return result;
            }
        }

        #endregion

        private static bool HasRequiredAttribute(IPropertyAccessor property, out RequiredAttribute validator)
        {
            validator = property.Attributes
                .OfType<RequiredAttribute>()
                .FirstOrDefault();

            return validator != null;
        }

        private ValidationResult Validate(ValidationAttribute validator, string propertyName, object propertyValue, string parentPropertyName)
        {
            var isValid = validator.IsValid(propertyValue);

            if (isValid)
            {
                return ValidationResult.Success;
            }

            // Prepare error message.
            var errorMessage = validator.FormatErrorMessage(propertyName);

            // Prepare member names, considering parent property.
            var memberNames = BuildMemberNames(propertyName, parentPropertyName);

            return new ValidationResult(errorMessage, memberNames);
        }

        private static string[] BuildMemberNames(string propertyName, string parentPropertyName)
        {
            return parentPropertyName == null
                ? new[] { propertyName }
                : new[] { $"{parentPropertyName}.{propertyName}" };
        }
    }
}
