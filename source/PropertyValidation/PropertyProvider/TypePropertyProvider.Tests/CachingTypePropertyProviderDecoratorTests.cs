using System.ComponentModel.Annotations.Validation.TypePropertyProvider.Caching;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace System.ComponentModel.Annotations.Validation.TypePropertyProvider.Tests
{
    [TestClass]
    public sealed class CachingTypePropertyProviderDecoratorTests
    {
        [SuppressMessage("Microsoft.Design", "IDE0052")] // TODO Remove after test implementation.
        private readonly ITypePropertyProvider sut;

        public CachingTypePropertyProviderDecoratorTests()
        {
            var objectCacheMock = Substitute.For<IObjectCache>();
            var propertyProviderMock = Substitute.For<ITypePropertyProvider>();
            sut = new CachingTypePropertyProviderDecorator(propertyProviderMock, objectCacheMock);
        }

        [TestMethod]
        public void Writes_to_the_cache_and_returns_the_properties_received_from_the_decorated_provider()
        {
            // Arrange
            // Act
            // Assert
            Assert.Inconclusive();
        }

        [TestMethod]
        public void Returns_properties_from_cache_if_they_have_already_been_requested_once()
        {
            // Arrange
            // Act
            // Assert
            Assert.Inconclusive();
        }
    }
}
