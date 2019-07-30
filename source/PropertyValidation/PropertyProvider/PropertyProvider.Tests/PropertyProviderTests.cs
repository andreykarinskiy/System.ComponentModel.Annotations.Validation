using System.Collections.Generic;
using System.ComponentModel.Annotations.Validation.PropertyProvider.Tests.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PropertyProvider;

namespace System.ComponentModel.Annotations.Validation.PropertyProvider.Tests
{
    [TestClass]
    [TestCategory("Acceptance")]
    public sealed class PropertyProviderTests
    {
        private readonly IPropertyProvider sut;

        public PropertyProviderTests()
        {
            sut = PropertyProviderFactory.CreateCached();
        }

        [DataTestMethod]
        [DataRow(typeof(ClassWithPropertiesOnly))]
        public void Must_return_a_collection_of_class_property_accessors(Type classType)
        {
            // Arrange
            var source = classType.Instantiate();

            // Act
            var accessors = sut.GetProperties(source);

            // Assert
            accessors.Should().BeEquivalentByName(classType.GetProperties());
        }

        [DataTestMethod]
        [DataRow(typeof(ClassWithPropertiesOnly))]
        public void Must_return_cached_accessor_if_it_has_already_been_created_once(Type classType)
        {
            // Arrange
            IEnumerable<object> AccessorsFunc(object o) => sut.GetProperties(o);

            var source = classType.Instantiate();

            // Act
            var (first, next) = (AccessorsFunc(source), AccessorsFunc(source));

            // Assert
            first.Should().BeEquivalentByItemsReference(next);
        }
    }
}
