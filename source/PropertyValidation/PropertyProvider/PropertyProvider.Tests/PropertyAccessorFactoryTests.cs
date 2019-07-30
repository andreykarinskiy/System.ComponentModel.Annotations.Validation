using System.ComponentModel.Annotations.Validation.PropertyProvider.Tests.TestData;
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
    public sealed class PropertyAccessorFactoryTests
    {
        private readonly IPropertyAccessorFactory sut;
        private readonly IObjectReferenceChecker referenceCheckerMock;

        public PropertyAccessorFactoryTests()
        {
            referenceCheckerMock = Substitute.For<IObjectReferenceChecker>();

            var collectionCheckerMock = Substitute.For<IObjectCollectionChecker>();

            var attributeProviderStub = Substitute.For<IMemberAttributeProvider>();

            // To test the logic of a factory, the providers it uses are not important, so they are replaced with stubs.
            sut = new LambdaPropertyAccessorFactory(referenceCheckerMock, collectionCheckerMock, attributeProviderStub);
        }

        [TestMethod]
        public void Should_create_PropertyAccessor_for_property()
        {
            // Arrange
            var property = new ClassWithPropertiesOnly()
                .Property(o => o.StringPropertyWithGetSet);
            
            // Act
            IPropertyAccessor accessor = sut.Create(property);

            // Assert
            accessor.Should().NotBeNull();
        }

        [TestMethod]
        public void Created_accessor_must_return_value_from_source_property()
        {
            // Arrange
            var source = new ClassWithPropertiesOnly();
            var property = source.Property(o => o.StringPropertyWithGetSet);

            // Act
            IPropertyAccessor accessor = sut.Create(property);

            // Assert
            accessor.GetValue(source).Should().Be(source.StringPropertyWithGetSet);
        }

        [TestMethod]
        public void Must_create_an_accessor_for_builtIn_types()
        {
            // Arrange
            var source = new ClassWithPropertiesOnly();
            var property = source.Property(o => o.StringPropertyWithGetSet);

            referenceCheckerMock
                .IsReference(property)
                .Returns(false);

            // Act
            IPropertyAccessor accessor = sut.Create(property);

            // Assert
            accessor.IsReference.Should().BeFalse();
        }

        [TestMethod]
        public void Must_create_an_accessor_for_references_to_other_objects()
        {
            // Arrange
            // Act
            // Assert
            Assert.Inconclusive();
        }
    }
}
