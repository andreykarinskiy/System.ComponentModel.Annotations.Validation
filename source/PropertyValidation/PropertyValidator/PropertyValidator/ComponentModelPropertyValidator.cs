using PropertyProvider;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using PropertyProvider.Accessor;

namespace System.ComponentModel.Annotations.Validation.PropertyValidator
{
    /// <summary>
    /// Wrapper for standard System.ComponentModel validator.
    /// </summary>
    [ExcludeFromCodeCoverage] // Because the class is needed only for comparison with the built-in validator.
    public class ComponentModelPropertyValidator : IPropertyValidator
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="source"></param>
        /// <param name="property"></param>
        /// <param name="parentProperty"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(object source, IPropertyAccessor property, IPropertyAccessor parentProperty)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(source) { MemberName = property.Name };

            var value = property.GetValue(source);
            Validator.TryValidateProperty(value, context, results);

            foreach (var result in results)
            {
                // TODO replace to NullObject
                if (parentProperty == null)
                {
                    yield return result;
                }
                else
                {
                    var memberNames = result.MemberNames.Select(n => $"{parentProperty.Name}.{n}");
                    yield return new ValidationResult(result.ErrorMessage, memberNames);
                }
            }
        }
    }
}
