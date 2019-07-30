using System.ComponentModel.Annotations.Validation.PropertyProvider.Tests.TestData;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PropertyProvider;
using PropertyProvider.Checkers;

namespace System.ComponentModel.Annotations.Validation.PropertyProvider.Tests
{
    [TestClass]
    public sealed class ObjectReferenceCheckerTests
    {
        private readonly IObjectReferenceChecker sut;

        public ObjectReferenceCheckerTests()
        {
            sut = new ReflectionReferenceChecker();
        }

        [TestMethod]
        public void When_property_type_is_custom_Then_returns_TRUE()
        {
            // Arrange
            var property = new ClassWithPropertiesAndReferences()
                .Property(o => o.ReferenceProperty);

            // Act
            var actual = sut.IsReference(property);

            // Assert
            actual.Should().BeTrue();
        }

        public void When_property_type_is_collection_of_custom_types_Then_returns_TRUE()
        {
            // Arrange
            var property = new ClassWithPropertiesAndReferences()
                .Property(o => o.CustomTypesCollection);

            // Act
            var actual = sut.IsReference(property);

            // Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void When_property_type_is_buildIn_Then_returns_FALSE()
        {
            // Arrange
            var property = new ClassWithPropertiesOnly()
                .Property(o => o.StringPropertyWithGetSet);

            // Act
            var actual = sut.IsReference(property);

            // Assert
            actual.Should().BeFalse();
        }
    }
}
