using System.Collections.Generic;
using System.ComponentModel.Annotations.Validation.MethodProvider;
using System.ComponentModel.Annotations.Validation.MethodProvider.Accessor;
using System.ComponentModel.Annotations.Validation.MethodValidator.Dispatcher;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace System.ComponentModel.Annotations.Validation.MethodValidator
{
    /// <inheritdoc cref="IMethodValidator"/>
    public sealed class DefaultMethodValidator : IMethodValidator
    {
        private readonly IMethodResultDispatcher methodResultDispatcher;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="methodResultDispatcher"></param>
        public DefaultMethodValidator(IMethodResultDispatcher methodResultDispatcher)
        {
            this.methodResultDispatcher = methodResultDispatcher;
        }

        /// <summary>
        /// default ctor
        /// </summary>
        public DefaultMethodValidator() : this(new DefaultMethodResultDispatcher())
        {
        }


        /// <inheritdoc cref="IMethodValidator.Validate"/>
        [method:SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")] 
        // Because instead of an exception, the result should return.
        public IEnumerable<ValidationResult> Validate(object source, IMethodAccessor method)
        {
            var validationResults = new List<ValidationResult>();

            if (method == null)
            {
                return validationResults;
            }

            try
            {
                // Gets invariant method result.
                var methodResult = method.Invoke(source);

                // Prepare validation result.
                methodResultDispatcher.DispatchResult(methodResult, method.InvariantName, validationResults);
            }
            catch (Exception exception)
            {
                // Prepare result from exception.
                methodResultDispatcher.DispatchResult(method.MethodName, exception, validationResults);
            }

            return validationResults;
        }
    }
}
