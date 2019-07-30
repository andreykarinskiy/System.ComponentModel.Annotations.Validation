using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sуstem.ComponentModel.Annotations.Validation
{
    /// <summary>
    /// Validates the properties and methods of an object.
    /// Validatable properties must be marked with attribute <see cref="ValidationAttribute"/>ValidationAttribute,
    /// and methods - marked with attribute <see cref="InvariantAttribute"/>InvariantAttribute.
    /// </summary>
    public interface IValidator
    {
        /// <summary>
        /// Validates the object and returns a collection of errors.
        /// </summary>
        /// <param name="source">source object</param>
        /// <returns>validation errors (maybe empty)</returns>
        IEnumerable<ValidationResult> Validate(object source);
    }
}
