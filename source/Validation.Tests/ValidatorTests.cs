using System.Collections.Generic;
using System.ComponentModel.Annotations.Validation.Tests.TestData;
using System.ComponentModel.DataAnnotations;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sуstem.ComponentModel.Annotations.Validation;

// ReSharper disable CoVariantArrayConversion due to FluentAssertions BeEquivalentTo method.

namespace System.ComponentModel.Annotations.Validation.Tests
{
    [TestClass]
    public sealed class ValidatorTests
    {
        private readonly IValidator sut;

        public ValidatorTests()
        {
            sut = new ObjectValidator();
        }

        [TestMethod]
        public void When_object_without_validation_Then_returns_no_errors()
        {
            // Arrange
            var source = new ClassWithoutValidation();

            // Act
            IEnumerable<ValidationResult> actual = sut.Validate(source);

            // Assert
            actual.Should().BeEmpty();
        }

        [TestMethod]
        public void When_object_with_validation_attributes_Then_returns_errors()
        {
            // Arrange
            ValidationResult[] expected = Expectation
                .Result("The RequiredStringProperty field is required.", "RequiredStringProperty")
                .Result("The field ComplexStringProperty must be a string or array type with a minimum length of '2'.", "ComplexStringProperty")
                .Result("The ComplexStringProperty field is not a valid e-mail address.", "ComplexStringProperty")
                .Result("The field RangedIntProperty must be between 21 and 65.", "RangedIntProperty");

            var source = new ClassWithInvalidProperties();

            // Act
            var actual = sut.Validate(source);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void When_object_without_invariants_Then_returns_no_errors()
        {
            // Arrange
            var source = new ClassWithoutInvariants();

            // Act
            var actual = sut.Validate(source);

            // Assert
            actual.Should().BeEmpty();
        }

        [TestMethod]
        public void When_object_with_invariants_Then_returns_errors()
        {
            // Arrange
            ValidationResult[] expected = Expectation
                .Result("InvariantA.", "InvariantA")
                .Result("InvariantB.", "InvariantB");

            var source = new ClassWithInvariants();

            // Act
            var actual = sut.Validate(source);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void When_object_with_validation_attributes_and_object_references_Then_returns_errors()
        {
            // Arrange
            ValidationResult[] expected = Expectation
                .Result("The StringProperty field is required.", "StringProperty")
                .Result("The RequiredStringProperty field is required.", "ReferenceProperty.RequiredStringProperty")
                .Result("The field ComplexStringProperty must be a string or array type with a minimum length of '2'.", "ReferenceProperty.ComplexStringProperty")
                .Result("The ComplexStringProperty field is not a valid e-mail address.", "ReferenceProperty.ComplexStringProperty")
                .Result("The field RangedIntProperty must be between 21 and 65.", "ReferenceProperty.RangedIntProperty")
                .Result("InvariantA.", "InvariantA")
                .Result("InvariantB.", "InvariantB");

            var source = new ClassWithInvalidPropertiesAndReferences();

            // Act
            var actual = sut.Validate(source);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void When_object_with_circular_object_reference_Then_returns_errors()
        {
            // Arrange
            ValidationResult[] expected = Expectation
                .Result("The StringProperty field is required.", "StringProperty")
                .Result("The StringProperty field is required.", "ReferenceProperty.StringProperty");

            var source = new ClassWithCircularReferences();
            var backReference = new ClassWithBackReference();

            // circular reference
            source.ReferenceProperty = backReference;
            backReference.ReferenceProperty = source;
            
            // Act
            var actual = sut.Validate(source);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
