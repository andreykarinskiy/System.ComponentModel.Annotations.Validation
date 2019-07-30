using System.Collections.Generic;
using System.ComponentModel.Annotations.Validation.MethodProvider.Accessor;
using System.Linq;
using System.Text;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;

namespace System.ComponentModel.Annotations.Validation.MethodProvider.Tests.TestData
{
    public static class MethodAccessorExtension
    {
        public static MethodAccessorAssertions Should(this IEnumerable<IMethodAccessor> sut)
        {
            return new MethodAccessorAssertions(sut);
        }
    }

    public sealed class MethodAccessorAssertions : ReferenceTypeAssertions<IEnumerable<IMethodAccessor>, MethodAccessorAssertions>
    {
        public MethodAccessorAssertions(IEnumerable<IMethodAccessor> subject)
        {
            Subject = subject;
        }

        #region Overrides of ReferenceTypeAssertions<IMethodAccessor,MethodAccessorAssertions>

        protected override string Identifier => "MethodAccessor";

        #endregion

        public AndConstraint<MethodAccessorAssertions> BeEquivalentByName(IEnumerable<dynamic> expectation,
            string because = "", params object[] becauseArgs)
        {
            var orderedSut = Subject.Select(o => o.MethodName).OrderBy(o => o).ToArray();
            var orderedExpectation = expectation.Select(o => o.Name).OrderBy(o => o).Cast<string>().ToArray();

            var sb = new StringBuilder();
            for (var i = 0; i < Math.Max(orderedSut.Length, orderedExpectation.Length); i++)
            {
                var left = i >= orderedSut.Length ? "not exists" : orderedSut[i];
                var right = i >= orderedExpectation.Length ? "not exists" : orderedExpectation[i];

                if (left.Equals(right))
                {
                    continue;
                }

                sb.AppendLine($"Mismatch pair: \"{left}\" - \"{right}\"");
            }

            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .ForCondition(true)
                .FailWith("You can't assert a null reference")
                .Then
                .Given(() => Subject)
                .ForCondition(o => sb.Length == 0)
                .FailWith(sb.ToString());

            return new AndConstraint<MethodAccessorAssertions>(this);
        }

        public AndConstraint<CollectionAssertions> BeEquivalentTo(IEnumerable<object> expectation, string because = "",
            params object[] becauseArgs)
        {
            var collectionAssertions = new CollectionAssertions(Subject);

            return collectionAssertions.BeEquivalentTo(expectation, because, becauseArgs);
        }
    }
}
