using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace System.ComponentModel.Annotations.Validation.PropertyProvider.Tests.TestData
{
    public static class ExpressionExtension
    {
        public static IEnumerable<string> MemberNames<T>(this T instance, params Expression<Func<T, object>>[] expressions)
        {
            return expressions.Select(expr => MemberName(expr.Body));
        }

        public static string MemberName(Expression expression)
        {
            switch (expression)
            {
                case null:
                    throw new ArgumentNullException();

                case MemberExpression expr:
                {
                    return expr.Member.Name;
                }

                case MethodCallExpression expr:
                {
                    return expr.Method.Name;
                }

                case UnaryExpression expr:
                {
                    return MemberName(expr);
                }

                default:
                    throw new ArgumentException();
            }
        }

        private static string MemberName(UnaryExpression unaryExpression)
        {
            if (!(unaryExpression.Operand is MethodCallExpression))
            {
                var operand = (MemberExpression)unaryExpression.Operand;
                var member = operand.Member;
                return member.Name;
            }

            var methodExpression = (MethodCallExpression)unaryExpression.Operand;

            return methodExpression.Method.Name;
        }
    }
}
