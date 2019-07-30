using System.Collections.Generic;
using System.ComponentModel.Annotations.Validation.MethodValidator.Dispatcher;
using System.ComponentModel.DataAnnotations;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace System.ComponentModel.Annotations.Validation.MethodValidator.Tests
{
    [TestClass]
    public sealed class MethodResultDispatcherTests
    {
        private readonly IMethodResultDispatcher sut;
        private readonly ICollection<ValidationResult> actualResults;

        public MethodResultDispatcherTests()
        {
            sut = new DefaultMethodResultDispatcher();
            actualResults = new List<ValidationResult>();
        }

        [TestMethod]
        public void When_unsupported_result_Then_nothing_happens()
        {
            // Arrange
            var methodResult = 1;

            // Act
            sut.DispatchResult(methodResult, default, actualResults);

            // Assert
            actualResults.Should().BeEmpty();
        }

        [TestMethod]
        public void When_result_is_a_ValidationResult_Then_it_is_added_to_results_collection()
        {
            // Arrange
            var methodResult = new ValidationResult("Error");

            // Act
            sut.DispatchResult(methodResult, default, actualResults);

            // Assert
            actualResults.Should().Contain(methodResult);
        }

        [TestMethod]
        public void When_result_is_a_collection_of_ValidationResult_Then_it_is_added_to_results_collection()
        {
            // Arrange
            var methodResult = new[] { new ValidationResult("Error") };

            // Act
            sut.DispatchResult(methodResult, default, actualResults);

            // Assert
            actualResults.Should().Contain(methodResult);
        }

        [TestMethod]
        public void When_result_is_a_string_Then_it_is_added_to_results_collection()
        {
            // Arrange
            var methodResult = "Error";
            const string invariantName = default;

            // Act
            sut.DispatchResult(methodResult, invariantName, actualResults);

            // Assert
            actualResults.Should().BeEquivalentTo(new ValidationResult("Error", new[] { invariantName }));
        }

        [TestMethod]
        public void When_result_is_a_boolean_true_Then_nothing_happens()
        {
            // Arrange
            const bool methodResult = true;

            // Act
            sut.DispatchResult(methodResult, default, actualResults);

            // Assert
            actualResults.Should().BeEmpty();
        }

        [TestMethod]
        public void When_result_is_a_boolean_false_Then_it_is_added_to_results_collection()
        {
            // Arrange
            const bool methodResult = false;

            // Act
            sut.DispatchResult(methodResult, "INVARIANT", actualResults);

            // Assert
            actualResults.Should().BeEquivalentTo(new ValidationResult("Broken invariant \"INVARIANT\".", new[] { "INVARIANT" }));
        }

        [TestMethod]
        public void When_result_is_a_Success_Then_nothing_happens()
        {
            // Arrange
            var methodResult = ValidationResult.Success;

            // Act
            sut.DispatchResult(methodResult, default, actualResults);

            // Assert
            actualResults.Should().BeEmpty();
        }

        [TestMethod]
        public void When_result_is_a_Fail_Then_it_is_added_to_results_collection()
        {
            // Arrange
            var methodResult = new ValidationResult("Error", new[] { "INVARIANT" });

            // Act
            sut.DispatchResult(methodResult, "INVARIANT", actualResults);

            // Assert
            actualResults.Should().BeEquivalentTo(new ValidationResult("Error", new[] { "INVARIANT" }));
        }
    }
}