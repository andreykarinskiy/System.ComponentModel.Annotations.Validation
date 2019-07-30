using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace System.ComponentModel.Annotations.Validation.MethodValidator.Dispatcher
{
    /// <inheritdoc cref="IMethodResultDispatcher"/>
    public sealed class DefaultMethodResultDispatcher : IMethodResultDispatcher
    {
        /// <inheritdoc cref="IMethodResultDispatcher.DispatchResult(object, string, ICollection{ValidationResult})"/>
        public void DispatchResult(object methodResult, string invariantName, ICollection<ValidationResult> validationResults)
        {
            switch (methodResult)
            {
                case null:
                    break;

                case ValidationResult result when result.IsSuccess():
                    break;

                case ValidationResult result when result.IsFail():
                    validationResults.AddResult(result.ErrorMessage, invariantName);
                    break;

                case ValidationResult result:
                    validationResults.Add(result);
                    break;

                case IEnumerable<ValidationResult> results:
                    validationResults.AddRange(results);
                    break;

                case string result:
                    validationResults.AddResult(result, invariantName);
                    break;

                case bool result when result == false:
                    validationResults.AddResult($"Broken invariant \"{invariantName}\".", invariantName);
                    break;

                default:
                    return;
            }
        }

        /// <inheritdoc cref="IMethodResultDispatcher.DispatchResult(string, Exception, ICollection{ValidationResult})"/>
        public void DispatchResult(string methodName, Exception exception, ICollection<ValidationResult> validationResults)
        {
            validationResults.Add(new ValidationResult($"Method {methodName} thrown a exception {exception.GetType().Name}"));
        }
    }
}
