using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Sуstem.ComponentModel.Annotations.Validation;
// ReSharper disable UnusedMember.Local

namespace System.ComponentModel.Annotations.Validation.Tests.TestData
{
    public class ClassWithInvariants
    {
        [SuppressMessage("Microsoft.Performance", "CA1822")]
        // Because it is invoked through reflection.
        [Invariant("InvariantA.")]
        private ValidationResult InvariantA()
        {
            return new ValidationResult("InvariantA.", new []{ "InvariantA" });
        }

        [SuppressMessage("Microsoft.Performance", "CA1822")]
        // Because it is invoked through reflection.
        [Invariant("InvariantB.")]
        private ValidationResult InvariantB()
        {
            return new ValidationResult("InvariantB.", new[] { "InvariantB" });
        }
    }
}
