using System.ComponentModel.Annotations.Validation.TypePropertyProvider.Tests.TestData;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// ReSharper disable UnassignedGetOnlyAutoProperty
// ReSharper disable UnusedAutoPropertyAccessor.Local
// ReSharper disable UnusedMember.Local

namespace System.ComponentModel.Annotations.Validation.TypePropertyProvider.Tests
{
    [TestClass]
    public sealed class TypePropertyProviderTests
    {
        private readonly ITypePropertyProvider sut;

        public TypePropertyProviderTests()
        {
            sut = new ReflectionTypePropertyProvider();
        }

        [TestMethod]
        public void Returns_public_properties_with_a_getter()
        {
            // Arrange
            var instance = new ClassWithManyProperties();

            var expectedProperties = Expectation.OfProperties(instance, 
                o => o.PublicPropWithGetter, 
                o => o.PublicPropWithGetterAndSetter);

            // Act
            var actualProperties = sut.GetProperties(instance);

            // Assert
            actualProperties.Should().BeEquivalentTo(expectedProperties);
        }

        private class ClassWithManyProperties
        {
            public string PublicPropWithGetter { get; }

            public string PublicPropWithGetterAndSetter { get; set; }

            public string PublicPropWithSetterOnly { private get; set; }

            protected string NonPublicProperty { get; set; }
        }
    }
}
