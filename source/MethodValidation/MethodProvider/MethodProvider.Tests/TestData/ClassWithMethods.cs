using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Sуstem.ComponentModel.Annotations.Validation;

// ReSharper disable UnusedMember.Local
[assembly:SuppressMessage("Microsoft.Design", "IDE0051:C# Private member is unused.")]

namespace System.ComponentModel.Annotations.Validation.MethodProvider.Tests.TestData
{
    public class ClassWithMethods
    {
        [Invariant]
        private ValidationResult InvariantMethod()
        {
            return new ValidationResult("FAILED", new [] { "InvariantMethod" });
        }

        private ValidationResult SimpleMethod()
        {
            return new ValidationResult("FAILED", new[] { "SimpleMethod" });
        }
    }
}
