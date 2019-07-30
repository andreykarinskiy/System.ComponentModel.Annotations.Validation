using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace System.ComponentModel.Annotations.Validation.MethodValidator.Dispatcher
{
    /// <summary>
    /// Handles a call to an invariant method,
    /// turning its return value into an ValidationResult object.
    /// </summary>
    public interface IMethodResultDispatcher
    {
        /// <summary>
        /// Handle invariant method result.
        /// </summary>
        /// <param name="methodResult"></param>
        /// <param name="invariantName"></param>
        /// <param name="validationResults"></param>
        void DispatchResult(object methodResult, string invariantName, ICollection<ValidationResult> validationResults);

        /// <summary>
        /// Handle invariant method exception.
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="exception"></param>
        /// <param name="validationResults"></param>
        void DispatchResult(string methodName, Exception exception, ICollection<ValidationResult> validationResults);
    }
}
