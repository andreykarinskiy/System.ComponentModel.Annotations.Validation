using System.Collections.Generic;
using System.ComponentModel.Annotations.Validation.PropertyValidator.Tests.TestData;
using System.ComponentModel.DataAnnotations;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PropertyProvider;
using PropertyProvider.Accessor;

namespace System.ComponentModel.Annotations.Validation.PropertyValidator.Tests
{
    [TestClass]
    public sealed class PropertyValidatorTests
    {
        private readonly IPropertyValidator sut;

        public PropertyValidatorTests()
        {
            sut = new DefaultPropertyValidator();
        }

        [DataTestMethod]
        [DynamicData(nameof(GetTestParameters), DynamicDataSourceType.Method)]
        public void When_validating_a_any_properties_Should_return_results(object source, IPropertyAccessor property, IPropertyAccessor parentProperty, string because, IEnumerable<ValidationResult> expectedResults)
        {
            // Arrange, Act
            var actualResults = sut.Validate(source, property, parentProperty);

            // Assert
            actualResults.Should().BeEquivalentTo(expectedResults, because);
        }

        private static IEnumerable<object[]> GetTestParameters()
        {
            yield return new Expected("without validation")
                .FromSource<ClassWithProperties>()
                .OfProperty("StringPropertyWithoutValidation")
                .HasNoResult()
                .Build();

            yield return new Expected("maximum string length must be lesser than 10")
                .FromSource<ClassWithProperties>()
                .OfProperty("StringProperty", "success")
                .WithAttributes(new MaxLengthAttribute(10))
                .HasSucceededResult()
                .Build();

            yield return new Expected("with Required attribute")
                .FromSource<ClassWithProperties>()
                .OfProperty("StringProperty")
                .WithAttributes(new RequiredAttribute())
                .HasResult("The StringProperty field is required.")
                .Build();

            yield return new Expected("minimum string length must be at least 10, but actual length is 6")
                .FromSource<ClassWithProperties>()
                .OfProperty("StringProperty", value: "lesser")
                .WithAttributes(new MinLengthAttribute(10))
                .HasResult("The field StringProperty must be a string or array type with a minimum length of '10'.")
                .Build();

            yield return new Expected("validation member names builds from property name and parent property name")
                .FromSource<ClassWithProperties>()
                .OfProperty("StringProperty")
                .AndParentProperty("StringPropertyWithoutValidation")
                .WithAttributes(new RequiredAttribute())
                .HasResult("The StringProperty field is required.", propertyNames: "StringPropertyWithoutValidation.StringProperty")
                .Build();
        }
    }
}
