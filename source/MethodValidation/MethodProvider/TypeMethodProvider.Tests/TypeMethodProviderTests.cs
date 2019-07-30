using System.ComponentModel.Annotations.Validation.TypeMethodProvider.Tests.TestData;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// ReSharper disable UnusedMember.Local

namespace System.ComponentModel.Annotations.Validation.TypeMethodProvider.Tests
{
    [TestClass]
    public sealed class TypeMethodProviderTests
    {
        private readonly ITypeMethodProvider sut;

        public TypeMethodProviderTests()
        {
            sut = new ReflectionTypeMethodProvider();
        }

        [TestMethod]
        public void Returns_all_private_methods()
        {
            // Arrange
            var source = new ClassWithManyMethods();

            var expected = Expectation.OfMethods(source,
                "NonPublicMethod",
                "NonPublicStaticMethod");

            // Act
            var actual = sut.GetMethods(source);

            // Assert
            actual.Should().BeEquivalentTo(expected).And.HaveCount(2);
        }

        [Ignore("ExpectationBuilder incorrect work")]
        [TestMethod]
        public void Returns_all_private_methods_included_ancestor()
        {
            // Arrange
            var source = new ChildClass();

            var expected = Expectation.OfMethods(source,
                "NonPublicMethod",
                "NonPublicStaticMethod",
                "NonPublicChildMethod");

            // Act
            var actual = sut.GetMethods(source);

            // Assert
            actual.Should().BeEquivalentTo(expected).And.HaveCount(3);
        }

        private class ClassWithManyMethods
        {
            public void PublicMethod()
            {
            }

            private void NonPublicMethod()
            {
            }

            private static void NonPublicStaticMethod()
            {
            }
        }

        private sealed class ChildClass : ClassWithManyMethods
        {
            private void NonPublicChildMethod()
            {
            }
        }
    }
}
