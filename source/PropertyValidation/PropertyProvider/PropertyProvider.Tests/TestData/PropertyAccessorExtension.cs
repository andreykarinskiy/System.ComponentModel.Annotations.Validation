using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using PropertyProvider.Accessor;

namespace System.ComponentModel.Annotations.Validation.PropertyProvider.Tests.TestData
{
    public static class PropertyAccessorExtension
    {
        public static PropertyAccessorAssertions Should(this IEnumerable<IPropertyAccessor> sut)
        {
            return new PropertyAccessorAssertions(sut);
        }
    }

    public sealed class PropertyAccessorAssertions : ReferenceTypeAssertions<IEnumerable<IPropertyAccessor>, PropertyAccessorAssertions>
    {
        public PropertyAccessorAssertions(IEnumerable<IPropertyAccessor> subject)
        {
            Subject = subject;
        }

        #region Overrides of ReferenceTypeAssertions<IPropertyAccessor,PropertyAccessorAssertions>

        protected override string Identifier => "PropertyAccessor";

        #endregion

        public AndConstraint<PropertyAccessorAssertions> BeEquivalentByName(IEnumerable<dynamic> expectation,
            string because = "", params object[] becauseArgs)
        {
            var orderedSut = Subject.Select(o => o.Name).OrderBy(o => o).ToArray();
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

            return new AndConstraint<PropertyAccessorAssertions>(this);
        }
    }
}
