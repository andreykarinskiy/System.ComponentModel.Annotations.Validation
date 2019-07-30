using System.Collections.Generic;
using System.ComponentModel.Annotations.Validation.MethodValidator.Dispatcher;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace System.ComponentModel.Annotations.Validation.MethodValidator.Tests.TestData
{
    public sealed class Expectation
    {
        private readonly string because;
        private readonly ICollection<ValidationResult> expectedResults = new List<ValidationResult>();

        private object source;
        private ReflectionMethodAccessor methodAccessor;

        public Expectation(string because)
        {
            this.because = because;
        }

        public Expectation FromSource<TSource>() where TSource : class, new()
        {
            source = new TSource();

            return this;
        }

        public Expectation OnMethod(string methodName)
        {
            var method = source.Method(methodName);
            methodAccessor = new ReflectionMethodAccessor(method);

            return this;
        }

        public Expectation HasNoResults()
        {
            return this;
        }

        public Expectation HasResult(string errorMessage, string invariantName)
        {
            methodAccessor.InvariantName = invariantName;
            expectedResults.AddResult(errorMessage, methodAccessor.InvariantName);

            return this;
        }

        public object[] NotImplemented()
        {
            Assert.Inconclusive($": \"{because}\"");

            return null;
        }

        public object[] Build()
        {
            return new[]
            {
                source,
                methodAccessor,
                because,
                expectedResults
            };
        }
    }
}
