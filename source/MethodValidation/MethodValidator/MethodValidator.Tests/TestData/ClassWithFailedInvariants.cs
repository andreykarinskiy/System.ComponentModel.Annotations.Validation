using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace System.ComponentModel.Annotations.Validation.MethodValidator.Tests.TestData
{
    public class ClassWithFailedInvariants
    {
        [SuppressMessage("Microsoft.Performance", "CA1822")]
        public ValidationResult InvariantA()
        {
            return new ValidationResult("FAILED", new[] { "INVARIANT" });
        }
    }
}
