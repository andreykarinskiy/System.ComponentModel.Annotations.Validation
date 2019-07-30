using System.ComponentModel.Annotations.Validation.MethodProvider.Accessor;
using System.ComponentModel.Annotations.Validation.MethodProvider.AccessorFactory;
using System.ComponentModel.Annotations.Validation.MethodProvider.Tests.TestData;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace System.ComponentModel.Annotations.Validation.MethodProvider.Tests
{
    [TestClass]
    public sealed class MethodAccessorFactoryTests
    {
        private readonly IMethodAccessorFactory sut;

        public MethodAccessorFactoryTests()
        {
            sut = new LambdaMethodAccessorFactory();
        }

        [TestMethod]
        public void Should_create_MethodAccessor_for_method()
        {
            // Arrange
            var method = new ClassWithMethods().Method("InvariantMethod");

            // Act
            IMethodAccessor accessor = sut.Create(method);

            // Assert
            accessor.Should().NotBeNull();
        }
    }
}
