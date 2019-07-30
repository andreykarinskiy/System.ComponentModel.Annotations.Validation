using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using FluentAssertions.Primitives;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace System.ComponentModel.Annotations.Validation.PropertyProvider.Tests.TestData
{
    public static class CollectionExtension
    {
        public static CollectionAssertions Should(this IEnumerable<object> actual)
        {
            return new CollectionAssertions(actual);
        }
    }

    public sealed class CollectionAssertions : ReferenceTypeAssertions<IEnumerable<object>, CollectionAssertions>
    {
        public CollectionAssertions(IEnumerable<object> subject)
        {
            Subject = subject;
        }

        #region Overrides of ReferenceTypeAssertions<IEnumerable<object>,CollectionAssertions>

        protected override string Identifier => "Collection";

        #endregion

        public AndConstraint<CollectionAssertions> BeEquivalentByItemsReference(IEnumerable<object> expectation, string because = "", params object[] becauseArgs)
        {
            CollectionAssert.AreEquivalent(Subject.ToList(), expectation.ToList());

            return new AndConstraint<CollectionAssertions>(this);
        }
    }
}
