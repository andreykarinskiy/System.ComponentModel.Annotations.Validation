using System.Collections.Generic;
using System.ComponentModel.Annotations.Validation.MethodProvider;
using System.ComponentModel.Annotations.Validation.MethodProvider.Accessor;
using System.ComponentModel.DataAnnotations;

namespace System.ComponentModel.Annotations.Validation.MethodValidator
{
    /// <summary>
    /// Validator to check invariant methods.
    /// </summary>
    public interface IMethodValidator
    {
        /// <summary>
        /// Validate method of object instance.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        IEnumerable<ValidationResult> Validate(object source, IMethodAccessor method);
    }
}
