using System.ComponentModel.Annotations.Validation.PropertyProvider.Tests.TestData;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PropertyProvider;
using PropertyProvider.Checkers;

namespace System.ComponentModel.Annotations.Validation.PropertyProvider.Tests
{
    [TestClass]
    public sealed class ObjectCollectionCheckerTests
    {
        private readonly IObjectCollectionChecker sut;

        public ObjectCollectionCheckerTests()
        {
            sut = new ReflectionCollectionChecker();
        }

        [TestMethod]
        public void When_property_type_is_collection_Then_returns_TRUE()
        {
            // Arrange
            var property = new ClassWithPropertiesAndReferences()
                .Property(o => o.CustomTypesCollection);

            // Act
            var actual = sut.IsCollection(property);

            // Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void When_property_type_is_not_collection_Then_returns_FALSE()
        {
            // Arrange
            var property = new ClassWithPropertiesAndReferences()
                .Property(o => o.ReferenceProperty);

            // Act
            var actual = sut.IsCollection(property);

            // Assert
            actual.Should().BeFalse();
        }
    }
}
