using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using FluentAssertions.Primitives;

namespace System.ComponentModel.Annotations.Validation.MethodProvider.Tests.TestData
{
    public static class CollectionExtension
    {
        public static IEnumerable<object> AsMany(this object source)
        {
            return new[] { source };
        }

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

        public AndConstraint<CollectionAssertions> BeEquivalentTo(IEnumerable<object> expectation, string because = "", params object[] becauseArgs)
        {
            foreach (var right in expectation.OrderBy(o => o))
            {
                foreach (var left in Subject.OrderBy(o => o))
                {
                    left.Should().BeEquivalentTo(right);
                }
            }

            return new AndConstraint<CollectionAssertions>(this);
        }
    }
}
