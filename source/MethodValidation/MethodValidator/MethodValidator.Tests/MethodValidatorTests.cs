using System.Collections.Generic;
using System.ComponentModel.Annotations.Validation.MethodProvider;
using System.ComponentModel.Annotations.Validation.MethodProvider.Accessor;
using System.ComponentModel.Annotations.Validation.MethodValidator.Tests.TestData;
using System.ComponentModel.DataAnnotations;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace System.ComponentModel.Annotations.Validation.MethodValidator.Tests
{
    [TestClass]
    public sealed class MethodValidatorTests
    {
        private readonly IMethodValidator sut;

        public MethodValidatorTests()
        {
            sut = new DefaultMethodValidator();
        }

        [DataTestMethod]
        [DynamicData(nameof(GetTestParameters), DynamicDataSourceType.Method)]
        public void When_validating_a_any_invariant_methods_Should_return_results(object source, IMethodAccessor methodAccessor, string because, IEnumerable<ValidationResult> expected)
        {
            // Arrange, Act
            var actual = sut.Validate(source, methodAccessor);

            // Assert
            actual.Should().BeEquivalentTo(expected, because);
        }

        public static IEnumerable<object[]> GetTestParameters()
        {
            yield return new Expectation("without invariants")
                .FromSource<ClassWithoutInvariants>()
                .HasNoResults()
                .Build();

            yield return new Expectation("with success invariant")
                .FromSource<ClassWithSucceededInvariants>()
                .OnMethod("InvariantA")
                .HasNoResults()
                .Build();

            yield return new Expectation("with failed invariant")
                .FromSource<ClassWithFailedInvariants>()
                .OnMethod("InvariantA")
                .HasResult("FAILED", "INVARIANT")
                .Build();
        }
    }
}
