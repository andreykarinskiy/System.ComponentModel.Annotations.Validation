using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace System.ComponentModel.Annotations.Validation.MethodValidator.Dispatcher
{
    /// <summary>
    /// Fluent API for collections of ValidationResult.
    /// </summary>
    public static class ValidationResultExtension
    {
        /// <summary>
        /// Add result with error message and invariant name.
        /// </summary>
        /// <param name="validationResults"></param>
        /// <param name="errorMessage"></param>
        /// <param name="invariantName"></param>
        public static void AddResult(this ICollection<ValidationResult> validationResults, string errorMessage, string invariantName)
        {
            var validationResult = new ValidationResult(errorMessage, new[] { invariantName });
            validationResults.Add(validationResult);
        }

        /// <summary>
        /// Add result with invariant name to collection.
        /// </summary>
        /// <param name="validationResults"></param>
        /// <param name="result"></param>
        /// <param name="invariantName"></param>
        public static void AddResult(this ICollection<ValidationResult> validationResults, ValidationResult result, string invariantName)
        {
            AddResult(validationResults, result.ErrorMessage, invariantName);
        }

        /// <summary>
        /// Check if ValidationResult is Success.
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool IsSuccess(this ValidationResult result)
        {
            return IsOfType(result, "Success");
        }

        /// <summary>
        /// Check if ValidationResult is Fail.
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool IsFail(this ValidationResult result)
        {
            return IsOfType(result, "Fail");
        }

        private static bool IsOfType(ValidationResult result, string typeName)
        {
            return result.GetType().Name.Equals(typeName, StringComparison.Ordinal);
        }
    }
}
