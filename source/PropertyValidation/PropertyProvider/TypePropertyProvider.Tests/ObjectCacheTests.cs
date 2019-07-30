using System.ComponentModel.Annotations.Validation.TypePropertyProvider.Caching;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace System.ComponentModel.Annotations.Validation.TypePropertyProvider.Tests
{
    [TestClass]
    public class ObjectCacheTests
    {
        private readonly IObjectCache sut;

        public ObjectCacheTests()
        {
            sut = new ConcurrentObjectCache();
        }

        [TestMethod]
        public void GetOrAdd_with_new_key_should_save_value_to_cache()
        {
            // Arrange
            const int nonExistentKey = 1;

            var function = Substitute.For<Func<object>>();

            // Act
            sut.GetOrAdd(nonExistentKey, function);

            // Assert
            function.Received(1)();
        }

        [TestMethod]
        public void GetOrAdd_with_existing_key_should_returns_value_from_cache()
        {
            // Arrange
            const int key = 1;

            var function = Substitute.For<Func<object>>();

            sut.GetOrAdd(key, function);

            // Act
            sut.GetOrAdd(key, function);

            // Assert
            function.Received(1)();
        }

        [TestMethod]
        public void GetKey_returns_unique_key_for_instance()
        {
            // Arrange
            const int expectedKey = 1;

            var keySelector = Substitute.For<Func<object, IComparable>>();

            keySelector.Invoke(Arg.Any<object>()).Returns(expectedKey);

            sut.RegisterKeyProvider(keySelector);

            // Act
            var actualKey = sut.GetKey(Substitute.For<object>());

            // Assert
            actualKey.Should().Be(expectedKey);
        }
    }
}
