using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Microsoft.Design", "IDE0051:C# Private member is unused.")]

namespace System.ComponentModel.Annotations.Validation.MethodValidator.Tests.TestData
{
    public class ClassWithSucceededInvariants
    {
        [SuppressMessage("Microsoft.Performance", "CA1822")]
        // Because it is invoked through reflection.
        public ValidationResult InvariantA()
        {
            return ValidationResult.Success;
        }
    }
}
