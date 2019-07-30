using System.Collections.Generic;
using System.ComponentModel.Annotations.Validation.MethodProvider.Tests.TestData;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace System.ComponentModel.Annotations.Validation.MethodProvider.Tests
{
    [TestClass]
    public sealed class MethodProviderTests
    {
        private readonly IMethodProvider sut;

        public MethodProviderTests()
        {
            sut = MethodProviderFactory.CreateCached();
        }

        [DataTestMethod]
        [DataRow(typeof(ClassWithMethods))]
        public void Must_return_a_collection_of_class_method_accessors(Type classType)
        {
            // Arrange
            var expected = classType.Method("InvariantMethod").AsMany();

            var source = classType.Instantiate();

            // Act
            var actual = sut.GetMethods(source);

            // Assert
            actual.Should().BeEquivalentByName(expected);
        }

        [DataTestMethod]
        [DataRow(typeof(ClassWithMethods))]
        public void Must_return_methods_that_are_marked_with_an_Invariant_attribute(Type classType)
        {
            // Arrange
            var source = classType.Instantiate();

            // Act
            var accessors = sut.GetMethods(source);

            // Assert
            accessors.Should().BeEquivalentByName(classType.Methods().Where(m => m.Name.StartsWith("Invariant")));
        }

        [DataTestMethod]
        [DataRow(typeof(ClassWithMethods))]
        public void Must_return_cached_accessor_if_it_has_already_been_created_once(Type classType)
        {
            // Arrange
            IEnumerable<object> AccessorsFunc(object o) => sut.GetMethods(o);

            var source = classType.Instantiate();

            // Act
            var (first, next) = (AccessorsFunc(source), AccessorsFunc(source));

            // Assert
            first.Should().BeEquivalentTo(next);
        }
    }
}
