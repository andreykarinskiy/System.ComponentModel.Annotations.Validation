using System.ComponentModel.Annotations.Validation.PropertyProvider.Tests.TestData;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using PropertyProvider;
using PropertyProvider.Accessor;
using PropertyProvider.AccessorFactory;
using PropertyProvider.Checkers;
using PropertyProvider.MemberAttributes;

namespace System.ComponentModel.Annotations.Validation.PropertyProvider.Tests
{
    [TestClass]
    public sealed class PropertyAccessorTests
    {
        private readonly IPropertyAccessorFactory sutFactory;
        private readonly IMemberAttributeProvider attributeProviderMock;

        public PropertyAccessorTests()
        {
            var referenceCheckerMock = Substitute.For<IObjectReferenceChecker>();
            var collectionCheckerMock = Substitute.For<IObjectCollectionChecker>();

            attributeProviderMock = Substitute.For<IMemberAttributeProvider>();

            sutFactory = new LambdaPropertyAccessorFactory(referenceCheckerMock, collectionCheckerMock, attributeProviderMock);
        }

        [TestMethod]
        public void Returns_name_of_the_property()
        {
            // Arrange
            var property = new ClassWithPropertiesOnly()
                .Property(o => o.StringPropertyWithGetSet);

            IPropertyAccessor sut = sutFactory.Create(property);

            // Act
            var actual = sut.Name;

            // Assert
            actual.Should().Be(property.Name);
        }

        [TestMethod]
        public void Can_get_value_from_object_property()
        {
            // Arrange
            var source = new ClassWithPropertiesOnly();
            var property = source.Property(o => o.StringPropertyWithGetSet);

            IPropertyAccessor sut = sutFactory.Create(property);

            // Act
            var actualValue = sut.GetValue(source);

            // Assert
            actualValue.Should().Be(source.StringPropertyWithGetSet);
        }

        [TestMethod]
        public void Returns_property_attributes_if_any()
        {
            // Arrange
            var source = new ClassWithPropertiesOnly();
            var property = source.Property(o => o.StringPropertyWithGetSet);

            attributeProviderMock
                .GetAttributes(property)
                .Returns(Enumerable.Empty<Attribute>());

            IPropertyAccessor sut = sutFactory.Create(property);

            // Act
            var notPresent = sut.Attributes;

            // Assert
            AssertionExtensions.Should(notPresent).BeEmpty();
        }
    }
}
