using PropertyProvider;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PropertyProvider.Accessor;

namespace System.ComponentModel.Annotations.Validation.PropertyValidator
{
    /// <summary>
    /// Validator to check properties.
    /// Works including for nested properties.
    /// </summary>
    public interface IPropertyValidator
    {
        /// <summary>
        /// Validate property of object instance.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="property"></param>
        /// <param name="parentProperty"></param>
        /// <returns></returns>
        IEnumerable<ValidationResult> Validate(object source, IPropertyAccessor property, IPropertyAccessor parentProperty);
    }
}
