using System.ComponentModel.Annotations.Validation.MethodProvider.Accessor;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace System.ComponentModel.Annotations.Validation.MethodProvider.AccessorFactory
{
    /// <summary>
    /// Implementation of <see cref="IMethodAccessorFactory"/> interface.
    /// Creates fast method accessor instances from MethodInfo objects using System.Linq.Expressions.
    /// </summary>
    public sealed class LambdaMethodAccessorFactory : IMethodAccessorFactory
    {
        /// <inheritdoc cref="IMethodAccessorFactory.Create"/>
        public IMethodAccessor Create(MethodInfo methodInfo)
        {
            var methodName = methodInfo.Name;
            var invariantName = GetInvariantName(methodInfo);
            var accessor = CreateAccessor(methodInfo);

            return new DelegateMethodAccessor(methodName, invariantName, accessor);
        }

        private static string GetInvariantName(MethodInfo methodInfo)
        {
            var allAttributes = methodInfo
                .GetCustomAttributes(inherit: true)
                .Cast<Attribute>();

            dynamic invariantAttribute =
                allAttributes.SingleOrDefault(attr => attr.GetType().Name.Equals("InvariantAttribute", StringComparison.Ordinal));

            return invariantAttribute != null ? 
                invariantAttribute.Name : 
                methodInfo.Name;
        }

        private static Func<object, object> CreateAccessor(MethodInfo methodInfo)
        {
            if (methodInfo == null || methodInfo.DeclaringType == null)
            {
                throw new ArgumentNullException(nameof(methodInfo));
            }

            var typeObject = typeof(object);
            var obj = Expression.Parameter(typeObject, "obj");

            var convertProp = Expression.Convert(obj, methodInfo.DeclaringType);
            var methodCall = Expression.Call(convertProp, methodInfo);
            var methodCallConverted = Expression.Convert(methodCall, typeObject);

            var lambda = Expression.Lambda<Func<object, object>>(methodCallConverted, obj);

            return lambda.Compile();
        }
    }
}
