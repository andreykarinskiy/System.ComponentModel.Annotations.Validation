using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace System.ComponentModel.Annotations.Validation.Tests.TestData
{
    public static class Expectation
    {
        internal static ExpectationBuilder Result(string errorMessage, string memberName)
        {
            return new ExpectationBuilder(errorMessage, memberName);
        }

        internal sealed class ExpectationBuilder
        {
            private readonly ICollection<ValidationResult> expectedResults = new List<ValidationResult>();

            public ExpectationBuilder(string errorMessage, string memberName)
            {
                CreateResult(errorMessage, memberName);
            }

            // ReSharper disable once MemberHidesStaticFromOuterClass
            public ExpectationBuilder Result(string errorMessage, string memberName)
            {
                CreateResult(errorMessage, memberName);

                return this;
            }

            public static implicit operator ValidationResult[] (ExpectationBuilder builder)
            {
                return builder.expectedResults.ToArray();
            }
            
            private void CreateResult(string errorMessage, string memberName)
            {
                var memberNames = PrepareMemberNames(memberName);

                var validationResult = new ValidationResult(errorMessage, memberNames);
                expectedResults.Add(validationResult);
            }

            private static string[] PrepareMemberNames(string memberName)
            {
                return memberName
                    .Split('|')
                    .Select(s => s.Trim())
                    .ToArray();
            }
        }
    }
}
